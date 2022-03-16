using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControl : MonoBehaviour
{

    public float minX = -60f; //lower limit value for camera x-axis
    public float maxX = 60f; //upper limit value for camera x-axis
    public float sensitivity;
    float yRot = 0f;
    float xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        LookRotations();
    }

    void LookRotations()
    {
        yRot += Input.GetAxis("Mouse X") * sensitivity;
        xRot += Input.GetAxis("Mouse Y") * sensitivity;
        xRot = Mathf.Clamp(xRot, minX, maxX); //stop cam from turning over
        transform.localEulerAngles = new Vector3(-xRot, yRot, 0); //rotate the player
    }
}

