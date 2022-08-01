using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{

    public static bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            GameMusic.music.Pause();
            GameManager.instance.cam1.GetComponent<AudioListener>().enabled = false;
            GameManager.instance.cam2.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            GameMusic.music.UnPause();
            GameManager.instance.cam1.GetComponent<AudioListener>().enabled = true;
            GameManager.instance.cam2.GetComponent<AudioListener>().enabled = true;
        }        
    }
}
