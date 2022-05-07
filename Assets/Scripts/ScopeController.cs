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
    void Start()
    {
        instance = this;
    }

    void RaycastForTarget()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, fwd * 2000, Color.green);
        if (Physics.Raycast(transform.position, fwd, out hit, 2000))
        {
            if (hit.transform.CompareTag("ScopeTarget") && !hit.transform.CompareTag("Missile"))
            {
                targetedVehicle = hit.transform.parent.gameObject;
                targetName = "TARGET " + (EnemySpawner.instance.activeEnemies.IndexOf(targetedVehicle.transform.parent.gameObject) + 1);
                targetText.text = "TARGET LOCKED\n---------------\n" + targetName;
            }
        }
    }

    void FixedUpdate()
    {
        RaycastForTarget();
        if (targetedVehicle == null)
        {
            targetText.text = "NO TARGET\n---------------";
            if (!targetImg.activeSelf)
                targetImg.SetActive(true);
        } else
        {
            if (!targetedVehicle.GetComponent<UFO>().moveTargetImage)
            {
                targetText.text = "TRACKING LOST\n---------------";
            } else
            {
                targetText.text = "TARGET LOCKED\n---------------\n" + targetName;
            }
            targetedVehicle.GetComponent<UFO>().CheckCanvasActive();
            if (targetImg.activeSelf)
                targetImg.SetActive(false);            
        }        
    }
}
