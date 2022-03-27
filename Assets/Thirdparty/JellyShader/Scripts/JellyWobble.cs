using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyWobble : MonoBehaviour
{

    public float size;

    Renderer modelRenderer;
    float controlTime;
    public Transform wobbleOrigin;

    // Use this for initialization
    void Start()
    {
        wobbleOrigin = transform;

        modelRenderer = GetComponent<MeshRenderer>();
        controlTime = 0;
        modelRenderer.material.SetVector("_ModelOrigin", transform.position);
        modelRenderer.material.SetVector("_ImpactOrigin", wobbleOrigin.position);

        controlTime += Time.deltaTime;
        modelRenderer.material.SetFloat("_ControlTime", controlTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controlTime < 3f)
        {
            
        } else
        {
            enabled = false;
        }
    }
}
