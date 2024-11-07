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

    [HideInInspector] public int InstanceID;

    [HideInInspector] public string apiTestResult = ""; 

    public void TestApiConnection()
    {
        apiTestResult = "Testing API connection...";

        StartCoroutine(Get<VMPPingResult>("ping", success =>
        {
            apiTestResult = "API connection successful!";
        }, () =>
        {
            apiTestResult = "API connection failed!";
        }));
    }

    public void Register(string InstanceName)
    {
        StartCoroutine(Post<VMPInstanceRegisterResult>("instances/reg", new Dictionary<string, string>()
        {
            {"InstanceName",InstanceName}
        }, success =>
        {
            InstanceID = success.InstanceId;
        }));
    }
    
    public IEnumerator Get<TResult>(string endpoint, Action<TResult> success, Action failure = null)
    {
        string url = $"{ApiUrl}:{ApiPort}/v1/{endpoint}";
        Debug.Log(url);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            
            Debug.Log(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                TResult result = JsonUtility.FromJson<TResult>(request.downloadHandler.text);
                success(result);
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                failure();
            }
        }
    }
    
    public IEnumerator Post<TResult>(string endpoint, Dictionary<string, string> postData, Action<TResult> onResult, Action<string> onError = null)
    {
        string url = $"{ApiUrl}:{ApiPort}/v1/{endpoint}";
        
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
                onResult?.Invoke(result);
            }
            else
            {
                Debug.LogError("Request failed: " + request.error);
                onError?.Invoke(request.error); 
            }
        }
    }
}