using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeMissileStatus : MonoBehaviour
{
    public static ScopeMissileStatus instance;
    Text missileStatusText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {        
        missileStatusText = transform.GetComponent<Text>();
        missileStatusText.text = "ARMED";
    }

    public void CheckMissileActive()
    {
        if (!GameManager.instance.activeMissile)
        {
            missileStatusText.text = "ARMED";
        }
    }

    public IEnumerator UpdateText()
    {
        missileStatusText.text = "LAUNCHING...";
        yield return new WaitForSeconds(GameManager.instance.launchDelay);
        missileStatusText.text = "IN FLIGHT";
    }
}
