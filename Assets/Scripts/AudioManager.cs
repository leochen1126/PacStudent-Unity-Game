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
        // Start by playing the intro music
        PlayIntroMusic();
    }

    // Function to play intro music
    public void PlayIntroMusic()
    {
        musicSource.clip = introMusic;  // Set intro music clip
        musicSource.Play();             // Play intro music
        Invoke("PlayNormalGhostStateMusic", introMusic.length);  // After intro ends, play normal music
    }

    // Function to play normal ghost state music
    public void PlayNormalGhostStateMusic()
    {
        musicSource.clip = normalGhostStateMusic;  // Set normal ghost music
        musicSource.Play();                        // Play normal ghost music
        musicSource.loop = true;                   // Set it to loop
    }
}

