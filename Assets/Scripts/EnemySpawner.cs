using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<GameObject> enemyTypes;
    public List<GameObject> activeEnemies;

    private void Awake()
    {
        instance = this;
        activeEnemies = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (activeEnemies.Count < 3)
        {
            GameObject ufo = Instantiate(enemyTypes[Random.Range(0, 3)], transform.position, Quaternion.identity);
            float randScale = Random.Range(2f, 3f);
            ufo.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            ufo.transform.localScale = new Vector3(randScale, randScale, randScale);
            ufo.transform.GetChild(0).GetComponent<PathFollower>().speed = Random.Range(0.3f, 0.7f);
            activeEnemies.Add(ufo);
        }
    }
}
