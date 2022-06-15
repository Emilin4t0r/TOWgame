using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    public GameObject preBoard, scoreBoard, inputField;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnterNewScore()
    {
        HighScores.UploadScore(inputField.GetComponent<TMP_InputField>().text, ScoreSaver.savedScore);
        preBoard.SetActive(false);
    }
}
