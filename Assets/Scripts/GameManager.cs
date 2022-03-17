using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject missileSpawner;
    public GameObject moveTarget, missile;

    bool activeMissile;
    GameObject mslTemp, targetTemp;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!activeMissile)
                SpawnMissile();
            else
                ExplodeMissile();
        }
    }

    void SpawnMissile()
    {
        activeMissile = true;
        mslTemp = Instantiate(missile, missileSpawner.transform.position, missileSpawner.transform.localRotation, transform);
        targetTemp = Instantiate(moveTarget, missileSpawner.transform.position, missileSpawner.transform.parent.transform.parent.transform.localRotation, missileSpawner.transform);
    }

    void ExplodeMissile()
    {
        Destroy(mslTemp);
        Destroy(targetTemp);
        activeMissile = false;
    }
}
