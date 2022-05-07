using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOCanvas : MonoBehaviour
{
    private GameObject targetObject;
    Vector3 scopeCamPos;
    void Start()
    {
        targetObject = transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        scopeCamPos = GameManager.instance.cam2.transform.position;
        float distToTarget = Vector3.Distance(transform.position, scopeCamPos);
        targetObject.transform.position = Vector3.MoveTowards(transform.position, scopeCamPos, distToTarget);
    }
}
