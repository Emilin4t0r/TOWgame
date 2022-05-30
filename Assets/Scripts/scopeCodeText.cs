using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scopeCodeText : MonoBehaviour
{
    float timeToChange;
    Text codeText;
    List<string> codesList;
    public float spawnSpeed;

    private void Start()
    {
        codeText = transform.GetComponent<Text>();
        codesList = new List<string>();
    }

    private void FixedUpdate()
    {
        if (Time.time > timeToChange)
        {
            if (codesList.Count == 10) codesList.Remove(codesList[0]);
            string nextCode = "";            
            for (int i = 0; i < 8; ++i)
            {
                nextCode += Random.Range(0, 2).ToString();
            }
            codesList.Add(nextCode);
            string allCodes = "";
            foreach(string code in codesList)
            {
                allCodes += code + "\n";
            }

            codeText.text = allCodes;
            timeToChange += spawnSpeed;
        }
    }
}
