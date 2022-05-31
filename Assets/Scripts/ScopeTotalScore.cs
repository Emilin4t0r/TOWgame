using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeTotalScore : MonoBehaviour
{
    Text text;

    private void Start()
    {
        text = transform.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        text.text = "Total: " + Score.score.ToString();
    }
}
