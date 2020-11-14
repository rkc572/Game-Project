using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSounds : MonoBehaviour
{
    public AudioSource mageMovementAudioSource;
    public AudioSource mageVoxAudioSource;
    public AudioClip mageDamage;
    public AudioClip mageDeath;

    public AudioClip attack;

    private void Start()
    {
        mageMovementAudioSource = gameObject.AddComponent<AudioSource>();
        mageMovementAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        mageVoxAudioSource = gameObject.AddComponent<AudioSource>();
        mageVoxAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
    }

    public void PlayMovementClip(AudioClip clip)
    {
        mageMovementAudioSource.clip = clip;
        mageMovementAudioSource.Play();
    }

    public void PlayVoxClip(AudioClip clip)
    {
        mageVoxAudioSource.clip = clip;
        mageVoxAudioSource.Play();
    }

    public void playMageDamageSound()
    {
        PlayVoxClip(mageDamage);
    }

    public void playMageDeathSound()
    {
        PlayVoxClip(mageDeath);
    }

    public void playMageAttackSound()
    {
        PlayMovementClip(attack);
    }

}
