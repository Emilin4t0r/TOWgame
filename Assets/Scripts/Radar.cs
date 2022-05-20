using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public static Radar instance;

    public float interval;
    public GameObject blip;
    public GameObject rotPoint;
    public Transform blipParent;

    List<GameObject> targets;
    List<GameObject> blips;
    float nextSearchTime = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        blips = new List<GameObject>();
        targets = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (Time.time > nextSearchTime)
        {
            SearchForTargets();
            nextSearchTime = Time.time + interval;
        }
        float z = rotPoint.transform.localEulerAngles.y;
        blipParent.transform.localEulerAngles = new Vector3(0, 0, z);
    }

    public void RemoveFromTargets(GameObject target_)
    {
        targets.Remove(target_);
    }

    void SearchForTargets() //gets TWO instances of same enemy both as blip and as target. FIX
    {
        foreach (var blip in blips)
        {
            Destroy(blip.gameObject);
        }
        blips.Clear();
        Collider[] cols = Physics.OverlapSphere(transform.position, 10000);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Enemy") && !col.GetComponent<UFO>().hasBeenRadared)
            {
                targets.Add(col.gameObject);
                col.GetComponent<UFO>().hasBeenRadared = true;
            }
        }
        foreach(var target in targets)
        {
            GameObject blip_ = Instantiate(blip, transform.position, transform.localRotation, blipParent);
            blip_.transform.localEulerAngles = new Vector3(0, 0, 0);
            float x = target.transform.position.x / 3000;
            float z = target.transform.position.z / 3000;
            blip_.transform.localPosition = new Vector3(x, z, 0);
            blips.Add(blip_);
        }       
    }
}
