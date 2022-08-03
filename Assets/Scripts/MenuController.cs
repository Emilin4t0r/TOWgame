using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject explosionObj;
    public GameObject[] buttons;

    public GameObject mainMenu, optionsMenu, tutorialMenu;
    public Slider sfxSlider, musicSlider, mouseSensSlider;

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


    // Options

    public void ToggleOptions()
    {
        if (!optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
            UpdateSliders(SettingsSaver.sfxVol, SettingsSaver.musicVol, SettingsSaver.mouseSens);
        } else
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
            SaveSettings();
            buttons[1].SetActive(true);
        }
    }
    public void UpdateSliders(float _sfx, float _music, float _sens)
    {
        sfxSlider.value = _sfx;
        musicSlider.value = _music;
        mouseSensSlider.value = _sens;
    }
    public void SaveSettings()
    {
        SettingsSaver.UpdateSettings(sfxSlider.value, musicSlider.value, mouseSensSlider.value);
    }
}
