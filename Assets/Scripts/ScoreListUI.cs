using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreListUI : MonoBehaviour
{
    public static ScoreListUI instance;

    public GameObject listObj;
    public GameObject scoreText;

    private void Start()
    {
        instance = this;
    }

    public void AddNewScoreText(string msg)
    {
        GameObject temp = Instantiate(scoreText, listObj.transform);
        temp.GetComponent<TextMeshProUGUI>().text = msg;
    }
}
