using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VolumeVAlue : MonoBehaviour
{
    public Toggle VolumeToggle;
    public Slider VolumeSlider;
    public AudioSource Audio;
    public float volume;

    private void Start() {
        Load();
        ValueMusic();

    }

    public void SliderMusic(){
        volume = VolumeSlider.value;
        Save();
        ValueMusic();
    }
    public void ToggleMusic(){
        if(VolumeToggle.isOn==true){volume =1;}
        else{volume = 0;}
        Save();
        ValueMusic();
    }
    private void ValueMusic()
    {
        Audio.volume= volume;
        VolumeSlider.value=volume;
        if(volume==0){
            VolumeToggle.isOn=false;
        }else{
            VolumeToggle.isOn=true;
        }
    }

    private void Save(){
        PlayerPrefs.SetFloat("volume",volume);
    }

    private void Load(){
        volume = PlayerPrefs.GetFloat("volume",volume);
    }
}
