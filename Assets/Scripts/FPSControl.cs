﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControl : MonoBehaviour
{
    public float minX = -60f; //lower limit value for camera x-axis
    public float maxX = 60f; //upper limit value for camera x-axis
    public float sensitivity, scopedSensitivity;
    float yRot = 0f;
    float xRot = 0f;

    float multip;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CheckMultip();
    }

    void Update()
    {
        LookRotations();
    }

    public void CheckMultip()
    {
        if (GameManager.instance.scopedIn)
        {
            multip = scopedSensitivity;
        }
        else
        {
            multip = sensitivity;
        }
    }

    void LookRotations()
    {        
        yRot += Input.GetAxis("Mouse X") * multip * Time.deltaTime;
        xRot += Input.GetAxis("Mouse Y") * multip * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, minX, maxX); //stop cam from turning over
        transform.localEulerAngles = new Vector3(-xRot, yRot, 0); //rotate the player
    }
}

