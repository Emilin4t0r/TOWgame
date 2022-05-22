using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    AudioSource audioSource;
    public GameObject soundPrefab;
    public AudioClip[] sfx;

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();        
    }

    public void PlaySound(int audioIndex, float volume)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sfx[audioIndex], volume);        
    }

    public void SpawnSound(GameObject origin, int audioIndex, float volume)
    {
        var sound = Instantiate(soundPrefab, origin.transform.position, origin.transform.rotation);
        var aSource = sound.GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.PlayOneShot(sfx[audioIndex], volume);
    }

    public void SpawnSoundLoop(GameObject origin, int audioIndex, float volume)
    {
        var sound = Instantiate(soundPrefab, origin.transform.position, origin.transform.rotation, origin.transform);
        var aSource = sound.GetComponent<AudioSource>();
        aSource.loop = true;
        aSource.clip = sfx[audioIndex];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        aSource.volume = volume;
        aSource.Play();        
    }
}
