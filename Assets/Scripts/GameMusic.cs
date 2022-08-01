using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public static AudioSource music;
    private void Awake()
    {
        music = transform.GetComponent<AudioSource>();
    }    
}
