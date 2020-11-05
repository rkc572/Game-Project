using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	// Audio players components.
	public AudioMixer audioMixer;
	public AudioSource musicSource;

	// Singleton instance.
	public static AudioManager Instance = null;

	// Volume defaults
	const float masterVolumeDefault = 1;
	const float sfxVolumeDefault = 1;
	const float musicVolumeDefault = 1;

	// Mixer volume levels
	float masterVolumeLevel = 1;
	float sfxVolumeLevel = 1;
	float musicVolumeLevel = 1;


	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}

	// Reset Sounds Levels
	public void ResetVolumeLevelsToDefault()
	{
		masterVolumeLevel = masterVolumeDefault;
		sfxVolumeLevel = sfxVolumeDefault;
		musicVolumeLevel = musicVolumeDefault;

		var sliders = FindObjectsOfType<Slider>();
        foreach (Slider slider in sliders)
        {
            if (slider.tag == "VolumeSlider")
            {
				slider.value = 1;
            }
        }
	}

	public void UpdateSliderLevels()
	{
		var sliders = FindObjectsOfType<Slider>();
		foreach (Slider slider in sliders)
		{
			if (slider.tag == "VolumeSlider")
			{
				if (slider.name == "Master Slider")
                {
					slider.value = masterVolumeLevel;
				}
				else if (slider.name == "FX Slider")
                {
					slider.value = sfxVolumeLevel;
				}
				else if (slider.name == "Music Slider")
				{
					slider.value = musicVolumeLevel;
				}
			}
		}
	}

	void UpdateMixerLevels()
    {
		audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeLevel) * 20);
		audioMixer.SetFloat("FXVol", Mathf.Log10(sfxVolumeLevel) * 20);
		audioMixer.SetFloat("MusicVol", Mathf.Log10(musicVolumeLevel) * 20);
	}

	public void SetLevel(string levelName, float levelValue)
    {
        switch (levelName)
        {
			case "MasterVol":
				masterVolumeLevel = Mathf.Clamp(levelValue, 0, 1);
				break;
			case "FXVol":
				sfxVolumeLevel = Mathf.Clamp(levelValue, 0, 1);
				break;
			case "MusicVol":
				musicVolumeLevel = Mathf.Clamp(levelValue, 0, 1);
				break;
		}
    }

	private void Update()
    {
		UpdateMixerLevels();
    }
}
