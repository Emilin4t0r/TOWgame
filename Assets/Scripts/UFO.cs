using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UFO : MonoBehaviour
{
    GameObject missile;
    public GameObject canvas;
    public TextMeshProUGUI distText;
    public TextMeshProUGUI targetNameText;
    public List<Renderer> shapes;

    Slider slider;
    bool fadingIn;
    float timeToUpdateText;
    Color lastColor;
    public bool hasBeenRadared;
    public bool moveTargetImage;

    private void Start()
    {
        slider = canvas.transform.GetChild(0).transform.GetChild(2).transform.GetComponent<Slider>();
        StartCoroutine(WaitBeforeFadingIn());
        canvas.transform.SetParent(null, false);
        moveTargetImage = true;
        canvas.SetActive(false);
    }
    public void Kill()
    {
        Radar.instance.RemoveFromTargets(gameObject);
        transform.parent.GetComponent<Enemy>().Kill();
    }

    IEnumerator WaitBeforeFadingIn()
    {
        MakeInvisible();
        yield return new WaitForSeconds(1.5f);
        fadingIn = true;
    }

    void MakeInvisible()
    {
        lastColor = shapes[0].material.color;
        foreach (var shape in shapes)
        {
            Color color_ = new Color(lastColor.r, lastColor.g, lastColor.b, 0);
            shape.material.color = color_;
        }
    }

    void FadeOneStep()
    {
        lastColor = shapes[0].material.color;
        float fadeAmt = lastColor.a + (0.5f * Time.deltaTime);
        Color color = new Color(lastColor.r, lastColor.g, lastColor.b, fadeAmt);
        foreach (var shape in shapes)
        {
            shape.material.color = color;
        }
        if (fadeAmt >= 1)
        {
            fadingIn = false;
        }
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
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
                targetNameText.text = ScopeController.instance.targetName;
            }
        }
        else
        {
            if (canvas.activeSelf)
                canvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (fadingIn)
        {
            FadeOneStep();
        }

        if (GameManager.instance.scopedIn)
        {            
            CheckForVisibility();
            if (moveTargetImage)
            {
                canvas.transform.LookAt(Vector3.zero);
                float dist = Vector3.Distance(transform.position, Vector3.zero);
                canvas.transform.localScale = new Vector3(dist / 100, dist / 100, 1);
                canvas.transform.localPosition = transform.position;
                canvas.transform.Translate(0, 0, 10, Space.Self);
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
            if (canvas.activeSelf)
                canvas.SetActive(false);
        }
    }
}
