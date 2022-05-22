using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject explosionObj;
    public GameObject[] buttons;
    
    void StartGame() => SceneManager.LoadScene("GameScene");

    void QuitGame() => Application.Quit();

    void OpenOptions()
    {
        print("opening options!");
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
    public void QuitButton()
    {
        StartCoroutine(AnimationWaiter(() => QuitGame(), buttons[2].gameObject));
        buttons[2].SetActive(false);
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
}
