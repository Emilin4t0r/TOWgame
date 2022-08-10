using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject explosionObj;
    public GameObject[] buttons;

    public GameObject mainMenu, optionsMenu, tutorialMenu;
    public Slider sfxSlider, musicSlider, mouseSensSlider;
    public TMP_Dropdown quality;

    private void Awake()
    {
        Score.score = 0;
    }

    void StartGame() => SceneManager.LoadScene("GameScene");

    void QuitGame() => Application.Quit();

    void OpenOptions()
    {
        ToggleOptions();
    }

    void OpenTutorial()
    {
        print("opening tutorial");
    }

    public void StartButton()
    {
        StartCoroutine(AnimationWaiter(() => StartGame(), buttons[0].gameObject));
        buttons[0].SetActive(false);
    }
    public void OptionsButton()
    {
        StartCoroutine(AnimationWaiter(() => OpenOptions(), buttons[1].gameObject));
        buttons[1].SetActive(false);
    }
    public void TutorialButton()
    {
        StartCoroutine(AnimationWaiter(() => OpenTutorial(), buttons[2].gameObject));
        buttons[2].SetActive(false);
    }
    public void QuitButton()
    {
        StartCoroutine(AnimationWaiter(() => QuitGame(), buttons[3].gameObject));
        buttons[3].SetActive(false);
    }    
    public void OptionsBackButton()
    {
        StartCoroutine(AnimationWaiter(() => ToggleOptions(), buttons[4].gameObject));
        buttons[4].SetActive(false);
    }

    public IEnumerator AnimationWaiter(Action func, GameObject ufo)
    {
        ExplodeUFOImage(ufo.transform.position);
        yield return new WaitForSeconds(0.5f);
        func();
    }

    void ExplodeUFOImage(Vector3 pos)
    {
        GameObject expl = Instantiate(explosionObj, pos, Quaternion.identity);
        expl.GetComponent<Animator>().Play("menu_expl_anim");
    }

    void ResetButtons()
    {
        foreach(GameObject but in buttons)
        {
            but.SetActive(true);
        }
    }
    // Options

    public void ToggleOptions()
    {
        if (!optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            UpdateUIValues(SettingsSaver.sfxVol, SettingsSaver.musicVol, SettingsSaver.mouseSens, SettingsSaver.graphicsQuality);
            ResetButtons();
        } else
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            SaveSettings();
            ResetButtons();
        }
    }
    public void UpdateUIValues(float _sfx, float _music, float _sens, int _quality)
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
}
