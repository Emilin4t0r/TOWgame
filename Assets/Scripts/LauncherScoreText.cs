using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LauncherScoreText : MonoBehaviour
{
    static TextMeshProUGUI scoreText;
    private void Awake()
    {
        scoreText = transform.GetComponent<TextMeshProUGUI>();
    }
    public static void UpdateScore()
    {
        scoreText.text = Score.score.ToString();
    }
}
