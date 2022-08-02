using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsSaver : MonoBehaviour
{
    public static float sfxVol, musicVol, mouseSens;
    public enum GraphicsQuality { Low, Mid, High };
    public static GraphicsQuality graphicsQuality;

    private void Awake()
    {
        print("Called");
        sfxVol = 0.5f;
        musicVol = 0.5f;
        mouseSens = 0.5f;
    }

    // Called when updating settings in pause menu or main menu settings
    public static void UpdateSettings(float _sfx, float _music, float _sens)
    {
        sfxVol = _sfx;
        musicVol = _music;
        mouseSens = _sens;

        // Apply values to relative targets
        Scene curScene = SceneManager.GetActiveScene();
        string sceneName = curScene.name;
        if (sceneName == "MainMenu")
        {
            MenuMusic.music.volume = musicVol;
        }
        else if (sceneName == "GameScene")
        {
            GameMusic.music.volume = musicVol;
        }

        FPSControl.instance.CheckMultip();
    }
}
