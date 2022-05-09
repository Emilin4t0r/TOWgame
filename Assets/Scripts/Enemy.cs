using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
        Destroy(transform.GetChild(0).GetComponent<UFO>().canvasParent.gameObject);
        EnemySpawner.instance.activeEnemies.Remove(gameObject);        
    }
}
