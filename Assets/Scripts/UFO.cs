using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UFO : MonoBehaviour
{
    GameObject missile;
    public Color sliderColorNormal, sliderColorPast;
    public Image sliderFill;
    public GameObject canvasParent;
    public TextMeshProUGUI distText;
    public TextMeshProUGUI targetNameText;
    public TextMeshProUGUI distArrow;
    public List<Renderer> shapes;
    public float canvasSizeMultip = 500;
    public GameObject explodedUFO;

    Slider slider;
    float timeToUpdateText;
    public bool hasBeenRadared;
    public bool moveTargetImage;
    bool isKilled;

    private void Start()
    {
        slider = canvasParent.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetComponent<Slider>();
        canvasParent.transform.SetParent(null, false);
        moveTargetImage = true;
        StartCoroutine(SpawnIn());
        transform.GetComponent<SoundPlayer>().PlaySound(2, 1);
        transform.GetComponent<SoundPlayer>().SpawnSoundLoop(gameObject, 1, 0.2f);
    }
    public void Kill()
    {
        if (!isKilled)
        {
            isKilled = true;
            Radar.instance.RemoveFromTargets(gameObject);
            GameObject rubble = Instantiate(explodedUFO, transform.position, transform.rotation);
            rubble.GetComponent<ExplodedUFO>().moveDir = transform.forward;
            transform.GetComponent<SoundPlayer>().SpawnSound(gameObject, 0, 1);
            transform.parent.GetComponent<Enemy>().Kill();
        }
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
                if (!hit.transform.CompareTag("Missile") && !hit.transform.CompareTag("Enemy"))
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
            if (missile == null)
            {
                missile = GameObject.FindGameObjectWithTag("Missile");
                distArrow.text = "";
                sliderFill.color = sliderColorNormal;
            }
            else
            {
                float distFromMissile = Vector3.Distance(transform.position, missile.transform.position);
                float distLauncherToUFO = Vector3.Distance(GameManager.instance.transform.position, transform.position);
                float distLauncherToMissile = Vector3.Distance(GameManager.instance.transform.position, missile.transform.position);
                if (Time.time > timeToUpdateText)
                {
                    distText.text = distFromMissile.ToString("F2") + " m";
                    timeToUpdateText = Time.time + 0.2f;
                    // Checking distArrow placement
                    if (distLauncherToUFO > distLauncherToMissile) // Missile is NOT past UFO
                    {
                        if (distFromMissile > 500)
                        {
                            distArrow.text = ">";
                            distArrow.transform.localPosition = new Vector3(125, 0, 0);
                            sliderFill.color = sliderColorNormal;
                        }
                        else
                        {
                            distArrow.text = "";
                            sliderFill.color = sliderColorNormal;
                        }
                    }
                    else if (distLauncherToUFO < distLauncherToMissile) // Missile IS past UFO
                    {
                        distArrow.text = "<";
                        distArrow.transform.localPosition = new Vector3(-125, 0, 0);
                        sliderFill.color = sliderColorPast;
                    }
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
