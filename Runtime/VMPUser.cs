using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VMP.Runtime;

public class VMPUser : MonoBehaviour
{
    public UnityEvent OnDisconnected;
    
    private VMPController controller;
    private int userId;
    private void Awake()
    {
        controller = GameObject.FindObjectOfType<VMPController>();
    }

    public void Join(int instanceId, string Username)
    {
        StartCoroutine(controller.Post<VMPInstanceJoinResult>("instances/join", new Dictionary<string, string>()
        {
            { "instanceId", instanceId.ToString() },
            { "username", Username }
        }, success =>
        {
            Debug.Log("Joined instance");
            userId = success.UserId;
        }));
    }
}
