using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeMissileStatus : MonoBehaviour
{
    public static ScopeMissileStatus instance;
    Text missileStatusText;
    public bool waiting;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        missileStatusText = transform.GetComponent<Text>();
        missileStatusText.text = "ARMED";
    }

    public void CheckMissileActive()
    {
        if (!GameManager.instance.activeMissile)
        {
            missileStatusText.text = "ARMED";
        }
    }

    public IEnumerator LaunchText()
    {
        missileStatusText.text = "LAUNCHING...";
        yield return new WaitForSeconds(GameManager.instance.launchDelay);
        missileStatusText.text = "IN FLIGHT";
    }

    public IEnumerator SplashText()
    {
        missileStatusText.text = "-SPLASH-";
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
        CheckMissileActive();
    }
}
