using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    public GameObject preBoard, scoreBoard, inputField, scoreNumberUI, navButtons;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        string score = "SCORE " + ScoreSaver.savedScore.ToString();
        scoreNumberUI.GetComponent<TextMeshProUGUI>().text = score;
    }

    public void EnterNewScore()
    {
        HighScores.UploadScore(inputField.GetComponent<TMP_InputField>().text, ScoreSaver.savedScore);
        ScoreSaver.savedScore = 0;
        Score.score = 0;
        preBoard.SetActive(false);
        navButtons.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
