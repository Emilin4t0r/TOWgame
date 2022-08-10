using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    static bool fading;
    Image img;
    float t;
    public bool fadeIn;
    private void Awake()
    {
        img = transform.GetComponent<Image>();
        if (SceneManager.GetActiveScene().name == "EndScene")        
            StartFade();        
    }
    public static void StartFade()
    {
        fading = true;
    }
    private void FixedUpdate()
    {
        if (fading)
        {
            if (fadeIn && img.color.a < 1)
            {
                img.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
                t += 0.75f * Time.fixedDeltaTime;
            } else if (!fadeIn && img.color.a > 0)
            {
                img.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
                t += 0.35f * Time.fixedDeltaTime;
                if (img.color.a <= 0)
                {
                    fading = false;
                    img.gameObject.SetActive(false);
                }
            }
        }
    }
}
