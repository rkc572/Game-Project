using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public string audioName = "MasterVol";

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(audioName, Mathf.Log10(sliderValue) * 20);
    }
}