using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyTypes;
    List<GameObject> activeEnemies;

    private void Start()
    {
        activeEnemies = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (activeEnemies.Count < 3)
        {
            GameObject ufo = Instantiate(enemyTypes[Random.Range(0, 3)], transform.position, Quaternion.identity);
            float randScale = Random.Range(1f, 2f);
            ufo.transform.localScale = new Vector3(randScale, randScale, randScale);
            ufo.transform.GetChild(0).GetComponent<PathFollower>().speed = Random.Range(1f, 2f);
            activeEnemies.Add(ufo);
        }
    }
}
