using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    public bool isInMenu = false;
    public AudioClip sceneMusic;
    public AudioClip menuMusic;
    public AudioSource musicSource;
    public AudioSource fwSource;
    public AudioSource fxSource;
    public AudioSource uiSource;

    private void Awake()
    {
        CreateSingleton();
    }
    private void Start()
    {
        PlayMusic();
        
    }

    private void PlayMusic()
    {
        if(isInMenu == true)
        {
            Play(menuMusic, 1f, musicSource);
        }
        else
        {
            Play(sceneMusic, 1f, musicSource);
        }
    }

    public void Pause(bool pause, AudioSource source)
    {
        if (pause == true)
        {
            source.Pause();
        }
        else
        {
            source.Play();
        }
    }
    public void Play(AudioClip clip,float volume, AudioSource source)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void Stop(AudioSource source)
    {
        source.clip = null;
        source.volume = 1;
        source.Stop();
    }

    public void PlaySFX(AudioClip sound,float volume, AudioSource source)
    {       
        source.PlayOneShot(sound,volume);
    }
}
