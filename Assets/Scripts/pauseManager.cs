using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class pauseManager : MonoBehaviour
{

    public static bool isPaused;
    public Slider sfxSlider, musicSlider, mouseSensSlider;
    public TMP_Dropdown quality;
    public GameObject pauseCanvas;

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
            UpdateUIValues(SettingsSaver.sfxVol, SettingsSaver.musicVol, SettingsSaver.mouseSens, SettingsSaver.graphicsQuality);
            pauseCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            GameMusic.music.UnPause();
            GameManager.instance.cam1.GetComponent<AudioListener>().enabled = true;
            GameManager.instance.cam2.GetComponent<AudioListener>().enabled = true;
            pauseCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            SaveSettings();
        }        
    }

    #region pausemenu
    public void LoadMainMenu()
    {
        transform.GetComponent<SoundPlayer>().PlaySound(0, 1);
        TogglePause();        
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");        
    }

    public void Resume()
    {
        TogglePause();
        transform.GetComponent<SoundPlayer>().PlaySound(0, 1);
    }

    void UpdateUIValues(float _sfx, float _music, float _sens, int _quality)
    {
        sfxSlider.value = _sfx;
        musicSlider.value = _music;
        mouseSensSlider.value = _sens;
        quality.value = _quality;
    }

    public void SaveSettings()
    {
        SettingsSaver.UpdateSettings(sfxSlider.value, musicSlider.value, mouseSensSlider.value, quality.value);
    }
    #endregion
}
