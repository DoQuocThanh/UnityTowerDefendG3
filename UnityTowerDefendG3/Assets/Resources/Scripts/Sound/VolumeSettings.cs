using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    //set volume ban đầu khi start

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMucsicVolume();
            SetSFXVolume();
        }
    }
    public void SetMucsicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("music", volume);

    }
    private void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);

    }
    private void LoadVolume()
    {
        Debug.Log(musicSlider.value);
        Debug.Log(PlayerPrefs.GetFloat("sfx"));
        musicSlider.value = PlayerPrefs.GetFloat("sfx");
        SFXSlider.value = PlayerPrefs.GetFloat("music");
        SetMucsicVolume();
        SetSFXVolume();
    }
}
