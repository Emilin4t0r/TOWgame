using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UFO : MonoBehaviour
{
    GameObject missile;
    public TextMeshProUGUI distText;

    public void Kill()
    {
        transform.parent.GetComponent<Enemy>().Kill();
    }

    private void FixedUpdate()
    {
        if (missile == null)
        {
            missile = GameObject.FindGameObjectWithTag("Missile");
        }

        //distText.text = Vector3.Distance(transform.position, missile.transform.position).ToString();
    }
}
