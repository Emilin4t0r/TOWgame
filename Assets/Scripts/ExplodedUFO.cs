using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedUFO : MonoBehaviour
{
    public GameObject[] parts;
    public Vector3 moveDir;

    private void Start()
    {
        foreach(var part in parts)
        {
            part.transform.rotation = Random.rotation;
            float randForce = Random.Range(5, 20);
            part.GetComponent<Rigidbody>().AddForce(part.transform.forward * randForce, ForceMode.Impulse);
            float randRot = Random.Range(1, 10);
            part.GetComponent<Rigidbody>().AddTorque(new Vector3(randRot, randRot, randRot), ForceMode.Impulse);
        }

        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.Translate(moveDir * 20 * Time.deltaTime, Space.World);
    }
}
