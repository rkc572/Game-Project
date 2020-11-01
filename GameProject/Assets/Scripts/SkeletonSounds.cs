using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSounds : MonoBehaviour
{
    public AudioSource skeletonMovementAudioSource;
    public AudioSource skeletonVoxAudioSource;
    public AudioClip skeletonDamage;

    public AudioClip attack;

    private void Start()
    {
        skeletonMovementAudioSource = gameObject.AddComponent<AudioSource>();
        skeletonMovementAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        skeletonVoxAudioSource = gameObject.AddComponent<AudioSource>();
        skeletonVoxAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
    }

    public void PlayMovementClip(AudioClip clip)
    {
        skeletonMovementAudioSource.clip = clip;
        skeletonMovementAudioSource.Play();
    }

    public void PlayVoxClip(AudioClip clip)
    {
        skeletonVoxAudioSource.clip = clip;
        skeletonVoxAudioSource.Play();
    }

    public void playSkeletonDamageSound()
    {
        PlayVoxClip(skeletonDamage);
    }

    public void playSkeletonAttackSound()
    {
        PlayMovementClip(attack);
    }

}
