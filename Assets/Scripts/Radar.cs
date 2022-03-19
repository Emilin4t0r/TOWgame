using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public float interval;
    public GameObject blip;
    public Transform blipParent;

    List<Collider> targets;
    List<GameObject> blips;
    float nextSearchTime;

    private void Start()
    {
        blips = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (Time.time > nextSearchTime)
        {
            SearchForTargets();
            nextSearchTime = Time.time + interval;
        }
        float z = transform.parent.localEulerAngles.y;
        blipParent.transform.localEulerAngles = new Vector3(0, 0, z);
    }

    void SearchForTargets()
    {
        foreach (var blip in blips)
        {
            Destroy(blip.gameObject);
        }
        blips.Clear();
        targets = new List<Collider>();
        Collider[] cols = Physics.OverlapSphere(transform.position, 10000);
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Enemy"))
            {
                targets.Add(col);
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
