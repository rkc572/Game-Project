using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public string audioName;

    private void Awake()
    {
        AudioManager.Instance.UpdateSliderLevels();
    }

    public void SetLevel(float sliderValue)
    {
        AudioManager.Instance.SetLevel(audioName, sliderValue);
    }
}