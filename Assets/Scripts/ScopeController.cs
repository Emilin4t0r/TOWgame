using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeController : MonoBehaviour
{
    public static ScopeController instance;
    public GameObject targetedVehicle;
    public Text targetText;
    public GameObject targetImg;
    public string targetName = "";
    public float raycastInterval = 0.2f;
    float timeToRaycast;
    public bool trackingLost;
    void Start()
    {
        instance = this;
        transform.GetComponent<SoundPlayer>().SpawnSoundLoop(GameManager.instance.cam2, 0, 0.2f);
        gameObject.SetActive(false);
    }

    void RaycastForTarget()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, fwd * 2000, Color.green);
        if (Physics.Raycast(transform.position, fwd, out hit, 2000))
        {
            if (hit.transform.CompareTag("ScopeTarget"))
            {
                if (hit.transform.parent.gameObject != targetedVehicle)
                {
                    targetedVehicle = hit.transform.parent.gameObject;
                    targetName = "TARGET " + (EnemySpawner.instance.activeEnemies.IndexOf(targetedVehicle.transform.parent.gameObject) + 1);
                    targetText.text = "TARGET LOCKED\n---------------\n" + targetName;
                    transform.GetComponent<SoundPlayer>().PlaySound(1, 1);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Time.time > timeToRaycast)
        {
            RaycastForTarget();
            timeToRaycast = Time.time + raycastInterval;
        }
        if (targetedVehicle == null)
        {
            targetText.text = "NO TARGET\n---------------";
            trackingLost = false;
            if (!targetImg.activeSelf)
                targetImg.SetActive(true);
        }
        else
        {
            if (!targetedVehicle.GetComponent<UFO>().moveTargetImage)
            {
                targetText.text = "TRACKING LOST\n---------------";
                trackingLost = true;
            }
            else
            {
                targetText.text = "TARGET LOCKED\n---------------\n" + targetName;
                trackingLost = false;
            }
            targetedVehicle.GetComponent<UFO>().CheckCanvasActive();
            if (targetImg.activeSelf)
                targetImg.SetActive(false);
        }        
    }
}
