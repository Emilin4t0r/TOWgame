using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public Sprite [] images;
    public string[] texts;
    public Image pictureObj;
    public TextMeshProUGUI textObj;
    int curInstruction;

    public void Next()
    {
        if (curInstruction < images.Length - 1)
            ApplyNewInstruction(1);
    }
    public void Back()
    {
        if (curInstruction >= 1)
            ApplyNewInstruction(-1);
        else
            CloseTutorial();
    }

    void ApplyNewInstruction(int direction)
    {
        curInstruction += direction;
        pictureObj.sprite = images[curInstruction];
        textObj.text = texts[curInstruction];
    }

    void CloseTutorial()
    {
        gameObject.SetActive(false);
    }
}
