using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    public static AudioManeger Instance;
    public Sound[] masterSound, musicSound, sfxSound;
    public AudioSource masterSoucre, musicSource, sfxSource ;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void Start()
    //{
    //   // PlayMusic("Theme");
    //}
    public void PlayMusic(string name)
    {
        StopMusic();
        Sound s = Array.Find(musicSound, s => s.name == name);

        if (s == null)
        {
            Debug.Log("Sound Play Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
       StopMusic();
        Sound s = Array.Find(sfxSound, s => s.name == name);

        if (s == null)
        {
            Debug.Log("Sound SFX Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            sfxSource.Play();
        }
    }
    public void StopMusic()
    {
        sfxSource.Stop();
        musicSource.Stop();
        masterSoucre.Stop();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
