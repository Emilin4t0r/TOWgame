using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public void PlayHover()
    {
        transform.GetComponent<SoundPlayer>().PlaySound(1, 1);
    }
    public void PlayClick()
    {
        transform.GetComponent<SoundPlayer>().PlaySound(0, 1);
    }
    public void PlayArrow()
    {
        transform.GetComponent<SoundPlayer>().PlaySound(2, 2);
    }

}
