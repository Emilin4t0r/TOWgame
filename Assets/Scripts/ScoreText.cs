using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;
    public float startFadeTime = 2;
    public float fadeSpeed = 0.1f;
    float startTime;
    Color origColor;
    float fadeAmt;

    private void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        startTime = Time.time;
        origColor = text.color;
        fadeAmt = 1;
    }

    private void FixedUpdate()
    {
        if (Time.time > startTime + startFadeTime)
        {
            fadeAmt -= fadeSpeed * Time.fixedDeltaTime;
            text.color = new Color(origColor.r, origColor.g, origColor.b, fadeAmt);
            if (text.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
