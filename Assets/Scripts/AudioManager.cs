using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip clip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
        audioSource.clip = this.clip;    
        audioSource.Play(); 
    }
}
