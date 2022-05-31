using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeCodeText : MonoBehaviour
{
    public static ScopeCodeText instance;

    float timeToChange;
    Text codeText;
    List<string> codesList;
    public float spawnSpeed;

    private void Start()
    {
        instance = this;
        codeText = transform.GetComponent<Text>();
        codesList = new List<string>();
    }

    private void FixedUpdate()
    {
        if (Time.time > timeToChange)
        {
            string randCode = "";            
            for (int i = 0; i < 8; ++i)
            {
                randCode += Random.Range(0, 2).ToString();
            }
            AddNewCode(randCode);
            timeToChange += spawnSpeed;
        }
    }

    public void AddNewCode(string newCode)
    {
        if (codesList.Count == 10) codesList.Remove(codesList[0]);
        codesList.Add(newCode);
        string allCodes = "";
        foreach (string code in codesList)
        {
            allCodes += code + "\n";
        }
        codeText.text = allCodes;
    }
}
