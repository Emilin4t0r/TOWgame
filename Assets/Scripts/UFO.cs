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
    public List<Renderer> shapes;

    Slider slider;
    bool fadingIn;    
    float timeToUpdateText;
    Color lastColor;

    private void Start()
    {
        slider = canvas.transform.GetChild(2).transform.GetComponent<Slider>();
        StartCoroutine(WaitBeforeFadingIn());
    }
    public void Kill()
    {
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
            Color color_ = shape.material.color;
            color_ = new Color(lastColor.r, lastColor.g, lastColor.b, 0);
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

    private void FixedUpdate()
    {
        if (fadingIn)
        {
            FadeOneStep();
        }

        if (GameManager.instance.scopedIn)
        {
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
            float distFromMissile;
            canvas.transform.LookAt(Vector3.zero);
            float dist = Vector3.Distance(transform.position, Vector3.zero);
            canvas.transform.localScale = new Vector3(dist / 100, dist / 100, 1);
            canvas.transform.localPosition = Vector3.zero;
            canvas.transform.Translate(0, 0, 10, Space.Self);
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
            {
                canvas.SetActive(false);
            }
        }
    }
}
