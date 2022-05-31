using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScopeCoordUI : MonoBehaviour
{
    public Text text;

    void FixedUpdate()
    {
        text.text = DateTime.Now.ToString("HH:mm:ss") + "\n-------------\n37°14'19.4''\n115°48'42.9''W";
    }
}
