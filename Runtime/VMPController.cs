using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VMP.Runtime;

public class VMPController : MonoBehaviour
{
    public string ApiUrl;
    public int ApiPort = 2150;

    [HideInInspector] public string apiTestResult = ""; 

    public void TestApiConnection()
    {
        StartCoroutine(Get<VMPPingResult>("ping", result =>
        {
            if (result.result == "pong")
            {
                apiTestResult = "API connection successful!";
            }
            else
            {
                apiTestResult = $"Failed to connect";
            }
        }));
    }

    
    public IEnumerator Get<TResult>(string endpoint, Action<TResult> onResult)
    {
        string url = $"{ApiUrl}:{ApiPort}/{endpoint}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                TResult result = JsonUtility.FromJson<TResult>(request.downloadHandler.text);
                onResult(result);
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
            }
        }
    }
    
    public IEnumerator Post<TResult>(string endpoint, Dictionary<string, string> postData, Action<TResult> onResult, Action<string> onError = null)
    {
        string url = $"{ApiUrl}:{ApiPort}/{endpoint}";
        
        WWWForm form = new WWWForm();
        foreach (var entry in postData)
        {
            form.AddField(entry.Key, entry.Value);
        }

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                TResult result = JsonUtility.FromJson<TResult>(request.downloadHandler.text);
                onResult?.Invoke(result);  // Invoke success callback
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                onError?.Invoke(request.error); 
            }
        }
    }
}