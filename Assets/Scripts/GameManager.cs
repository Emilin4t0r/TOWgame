using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject missileSpawner;
    public GameObject moveTarget, missile;
    public GameObject cam1, cam2;
    public GameObject tubeRotator;
    public float launchDelay;
    public bool scopedIn;    

    private bool activeMissile;
    public GameObject mslTemp, targetTemp;

    public float playTime = 180;
    public float timeLeft;
    public int kills;
    float timeFromLastKill;
    public Image clockImage;

    void Awake()
    {
        instance = this;
        CamShaker.activeCam = cam1.name;
        timeLeft = playTime;
        timeFromLastKill = 100;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!activeMissile)
            {
                SpawnMissile();
            }
            else
            {
                ResetMissile();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeCam();
        }

        CountTime();
    }

    void CountTime()
    {
        timeLeft -= Time.deltaTime;
        clockImage.fillAmount = timeLeft / playTime;
    }

    void ChangeCam()
    {
        if (cam1.activeSelf)
        {
            cam2.SetActive(true);
            cam1.SetActive(false);
            CamShaker.activeCam = cam2.name;
            scopedIn = true;
        }
        else
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            CamShaker.activeCam = cam1.name;
            scopedIn = false;
        }
        tubeRotator.GetComponent<FPSControl>().CheckMultip();
    }

    void SpawnMissile()
    {
        activeMissile = true;
        mslTemp = Instantiate(missile, missileSpawner.transform.position, missileSpawner.transform.rotation, transform);
        targetTemp = Instantiate(moveTarget, missileSpawner.transform.position, missileSpawner.transform.parent.transform.parent.transform.localRotation, missileSpawner.transform);
        transform.GetComponent<SoundPlayer>().PlaySound(0, 1);
    }

    public void GetKill(UFO ufo)
    {
        kills++;
        Score.Increase(100, "Kill");
        if (!scopedIn) Score.Increase(100, "No scope");
        if (Vector3.Distance(transform.position, ufo.transform.position) > 500) Score.Increase(50, "Long range");
        if (Time.time - timeFromLastKill < 5) Score.Increase(50, "Combo");
        timeFromLastKill = Time.time;
        if (ScopeController.instance.trackingLost) Score.Increase(200, "Blind shot");        
    }

    public void ResetMissile()
    {
        if (mslTemp != null)
        {
            mslTemp.GetComponent<Missile>().BlowUp(true);
        }
        Destroy(targetTemp);
        activeMissile = false;
    }
}
