using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSounds : MonoBehaviour
{
    public AudioSource golemMovementAudioSource;
    public AudioSource golemVoxAudioSource;
    public AudioClip golemDamage;
    public AudioClip golemDeath;

    public AudioClip attack;

    private void Start()
    {
        golemMovementAudioSource = gameObject.AddComponent<AudioSource>();
        golemMovementAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        golemVoxAudioSource = gameObject.AddComponent<AudioSource>();
        golemVoxAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
    }

    public void PlayMovementClip(AudioClip clip)
    {
        golemMovementAudioSource.clip = clip;
        golemMovementAudioSource.Play();
    }

    public void PlayVoxClip(AudioClip clip)
    {
        golemVoxAudioSource.clip = clip;
        golemVoxAudioSource.Play();
    }

    public void PlayGolemDamageSound()
    {
        PlayVoxClip(golemDamage);
    }

    public void PlayGolemDeathSound()
    {
        PlayVoxClip(golemDeath);
    }

    public void PlayGolemAttackSound()
    {
        PlayMovementClip(attack);
    }

}
