using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    GameObject menuSelector;
    public float selectorRotation;
    void Start()
    {
        menuSelector = GameObject.Find("SelectorRotator");
    }

    private void OnMouseEnter()
    {
        menuSelector.transform.eulerAngles = new Vector3(0, 0, selectorRotation);
    }

    private void OnMouseExit()
    {
        menuSelector.transform.eulerAngles = Vector3.zero;
    }
}
