using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VMPController : MonoBehaviour
{
    public string ApiUrl;
    public int ApiPort = 2150;

    [HideInInspector] public string apiTestResult = ""; 

    public IEnumerator TestApiConnection()
    {
        string url = $"{ApiUrl}:{ApiPort}/ping";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                apiTestResult = "API connection successful!";
            }
            else
            {
                apiTestResult = $"Failed to connect: {request.error}";
            }
        }
    }
}