using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public string targetTag;
    private Transform target;
    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void Update()
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 lookDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);
        float dist = targetDirection.magnitude;
        transform.Translate(Vector3.forward * Time.deltaTime * speed * dist, Space.Self);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
