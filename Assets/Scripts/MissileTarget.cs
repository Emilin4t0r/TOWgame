using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTarget : MonoBehaviour
{
    public float speed, accSpeed, maxSpeed;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.fixedDeltaTime));
        if (speed < maxSpeed)
            speed = speed + accSpeed;
    }
}
