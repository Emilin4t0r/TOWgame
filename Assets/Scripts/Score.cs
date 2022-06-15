using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;

    public static void Increase(int amt, string text)
    {
        score += amt;
        string scoreInfo = text + " +" + amt.ToString();
        ScopeCodeText.instance.AddNewCode(scoreInfo);
        ScoreListUI.instance.AddNewScoreText(scoreInfo);
        ScoreSaver.savedScore = score;
    }
}
