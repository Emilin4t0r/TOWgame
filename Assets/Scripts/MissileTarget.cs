using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTarget : MonoBehaviour
{
    public float speed, accSpeed, maxSpeed;
    bool isStarted;

    private void Start()
    {
        StartCoroutine(startDelay());
    }

    IEnumerator startDelay()
    {
        yield return new WaitForSeconds(GameManager.instance.launchDelay / 1.5f);
        isStarted = true;
    }

    private void FixedUpdate()
    {
        if (isStarted)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.fixedDeltaTime));
            if (speed < maxSpeed)
                speed = speed + accSpeed;
        }
    }
}
