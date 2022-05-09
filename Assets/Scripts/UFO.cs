using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UFO : MonoBehaviour
{
    GameObject missile;
    public GameObject canvasParent;
    public TextMeshProUGUI distText;
    public TextMeshProUGUI targetNameText;
    public List<Renderer> shapes;
    public float canvasSizeMultip = 500; 

    Slider slider;
    float timeToUpdateText;
    public bool hasBeenRadared;
    public bool moveTargetImage;

    private void Start()
    {
        slider = canvasParent.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetComponent<Slider>();
        canvasParent.transform.SetParent(null, false);
        moveTargetImage = true;
        StartCoroutine(SpawnIn());
    }
    public void Kill()
    {
        Radar.instance.RemoveFromTargets(gameObject);
        transform.parent.GetComponent<Enemy>().Kill();
    }

    void CanvasOn(bool val)
    {
        if (!val)
            canvasParent.transform.GetChild(0).transform.localPosition = new Vector3(0, -10000, 0);
        else
            canvasParent.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
    }
    bool CheckCanvasOn()
    {
        if (canvasParent.transform.GetChild(0).transform.localPosition.y == -10000)
            return false;
        else return true;
    }

    IEnumerator SpawnIn()
    {
        yield return new WaitForSeconds(0.1f);
        CanvasOn(false);
    }

    void CheckForVisibility()
    {
        Vector3 launcherPos = new Vector3(0, 1, 0);
        Vector3 towardsLauncher = launcherPos - transform.position;
        RaycastHit hit;
        Debug.DrawRay(transform.position, towardsLauncher * 2000, Color.red);
        if (Physics.Raycast(transform.position, towardsLauncher, out hit, 2000))
        {
            if (hit.transform.CompareTag("Launcher"))
            {
                moveTargetImage = true;
            }
            else
            {
                if (!hit.transform.CompareTag("Missile"))
                {
                    moveTargetImage = false;
                }
            }
        }
    }

    public void CheckCanvasActive()
    {
        if (ScopeController.instance.targetedVehicle == gameObject)
        {
            if (!CheckCanvasOn())
            {
                CanvasOn(true);
                targetNameText.text = ScopeController.instance.targetName;
            }
        }
        else
        {
            if (CheckCanvasOn())
                CanvasOn(false);
        }
    }

    private void Update()
    {
        if (GameManager.instance.scopedIn)
        {
            CheckForVisibility();
            if (moveTargetImage)
            {
                canvasParent.transform.LookAt(Vector3.zero);
                float dist = Vector3.Distance(transform.position, Vector3.zero);
                canvasParent.transform.localScale = new Vector3(dist / canvasSizeMultip, dist / canvasSizeMultip, 1);
                canvasParent.transform.localPosition = transform.position;
                canvasParent.transform.Translate(0, 0, 10, Space.Self);
            }
            float distFromMissile;
            if (missile == null)
            {
                missile = GameObject.FindGameObjectWithTag("Missile");
            }
            else
            {
                distFromMissile = Vector3.Distance(transform.position, missile.transform.position);
                if (Time.time > timeToUpdateText)
                {
                    distText.text = distFromMissile.ToString("F2") + " m";
                    timeToUpdateText = Time.time + 0.2f;
                }
                slider.value = distFromMissile / 500;
                return;
            }
            slider.value = 1f;
            distText.text = "";
        }
        else
        {
            if (CheckCanvasOn())
                CanvasOn(false);
        }
    }
}
