using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CamShaker : MonoBehaviour
{
    public static string activeCam;

    /// <summary>
    /// Shakes the currently active camera by force amount
    /// </summary>
    /// <param name="force"> 1: light shake, 2: normal shake, 3: heavy shake, 4: long shake </param>
    public static void Shake(int force)
    {
        switch (force) {
            case 1:
            CameraShaker.GetInstance(activeCam).ShakeOnce(2, 5, 0, 0.5f);
                break;
            case 2:
                CameraShaker.GetInstance(activeCam).ShakeOnce(3, 15, 0, 1f);
                break;
            case 3:
                CameraShaker.GetInstance(activeCam).ShakeOnce(5, 30, 0, 1.5f);
                break;
            case 4:
                CameraShaker.GetInstance(activeCam).ShakeOnce(1, 5, 0, 2f);
                break;
            default:
                CameraShaker.GetInstance(activeCam).ShakeOnce(5, 10, 0, 1f);
                break;
        }
    }
}
