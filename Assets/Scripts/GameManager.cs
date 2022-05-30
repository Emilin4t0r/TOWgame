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
    public Image clockImage;

    void Awake()
    {
        instance = this;
        CamShaker.activeCam = cam1.name;
        timeLeft = playTime;
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
        } else
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

    public void ResetMissile()
    {
        if (mslTemp != null)
        {
            mslTemp.GetComponent<Missile>().BlowUp();
        }
        Destroy(targetTemp);
        activeMissile = false;
    }
}
