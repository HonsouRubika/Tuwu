using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    public bool isInMenu = false;
    public AudioSource musicSource;
    public AudioSource fwSource;
    public AudioSource fxSource;
    public AudioSource uiSource;

    public SoundBank soundBank;

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
            Play("menuMusic", musicSource);
        }
        else
        {
            Play("sceneMusic", musicSource);
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

    public void Play(string soundName, AudioSource source)
    {
        foreach(SoundClip clip in soundBank.clips)
        {
            if (clip.name == soundName)
            {
                source.clip = clip.clip;
                source.volume = clip.volume;
            }
        }

        source.Play();
    }

    public void Stop(AudioSource source)
    {
        source.clip = null;
        source.volume = 1;
        source.Stop();
    }

    public void PlaySFX(string soundName, AudioSource source)
    {

        foreach (SoundClip clip in soundBank.clips)
        {
            if (clip.name == soundName)
            {
                source.PlayOneShot(clip.clip, clip.volume);
            }
        }

    }
}
