using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioClip introMusic;
    public AudioClip normalGhostStateMusic;

    private void Start()
    {
        
        PlayIntroMusic();
    }

    public void PlayIntroMusic()
    {
        musicSource.clip = introMusic;  
        musicSource.Play();             
        Invoke("PlayNormalGhostStateMusic", introMusic.length);  
    }

    
    public void PlayNormalGhostStateMusic()
    {
        musicSource.clip = normalGhostStateMusic;  
        musicSource.Play();      
        
        musicSource.loop = true;                   
    }
}

