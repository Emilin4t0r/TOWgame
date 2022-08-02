using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public static AudioSource music;
    private void Awake()
    {
        music = transform.GetComponent<AudioSource>();
    }
}
