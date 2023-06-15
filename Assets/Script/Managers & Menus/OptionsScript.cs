using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    private void Awake()
    {
        print("sliders activated");
        float mVol = 1f;
        float sVol = 1f;
        if (PlayerPrefs.HasKey("musicVol"))
        {
            mVol = PlayerPrefs.GetFloat("musicVol");
        }
        else
        {
            print("music key not found");
        }
        musicSlider.value = mVol;

        if (PlayerPrefs.HasKey("sfxVol"))
        {
            sVol = PlayerPrefs.GetFloat("sfxVol");
        }
        else
        {
            print("sfx key not found");
        }
        sfxSlider.value = sVol;
    }

    private void Start()
    {
        /* print("sliders activated");
         float mVol= 0.5f;
         float sVol = 0.5f;
         if (PlayerPrefs.HasKey("musicVol"))
         {
             mVol = PlayerPrefs.GetFloat("musicVol");
         }
         else
         {
             print("music key not found");
         }
         musicSlider.value = mVol;

         if (PlayerPrefs.HasKey("sfxVol"))
         {
             sVol = PlayerPrefs.GetFloat("sfxVol");
         }
         else
         {
             print("sfx key not found");
         }
         sfxSlider.value = sVol; */

    }
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic(); // not the same as the method above: comes from AudioManager
    }
    public void MusicVolumee()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("musicVol", musicSlider.value);


    }
    public void SFXVolumee()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVol", sfxSlider.value);
    }
}
