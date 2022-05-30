using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scopeTimeKillsUI : MonoBehaviour
{
    Text text;
    string timeLeft;
    string kills;
    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        int tempTime = (int)GameManager.instance.timeLeft;
        int minutes, seconds;
        minutes = tempTime / 60;
        seconds = tempTime - minutes * 60;
        if (seconds < 10)
        {
            timeLeft = "0" + minutes + ":0" + seconds;
        } else
        {
            timeLeft = "0" + minutes + ":" + seconds;
        }        
        text.text = "Time left: " + timeLeft + "\n-------------\nConfirmed: " + GameManager.instance.kills;
    }
}
