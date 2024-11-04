using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMPUser : MonoBehaviour
{
    private VMPController controller;
    private void Awake()
    {
        controller = GameObject.FindObjectOfType<VMPController>();
    }

    public void Register(int instanceId, string Username)
    {
        
    }
}
