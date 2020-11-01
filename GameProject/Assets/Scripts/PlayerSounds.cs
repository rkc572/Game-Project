using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource playerMovementAudioSource;
    public AudioSource playerVoxAudioSource;

    public AudioClip playerDamage;

    public AudioClip leftFootstep;
    public AudioClip rightFootstep;

    public AudioClip swordSwingOne;
    public AudioClip swordSwingTwo;
    public AudioClip swordSwingThree;
    public AudioClip swordSwingFour;

    AudioClip[] swordSwings;

    int swingCount = 0;
    float lastSwing;

    private void Start()
    {
        playerMovementAudioSource = gameObject.AddComponent<AudioSource>();
        playerMovementAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        playerVoxAudioSource = gameObject.AddComponent<AudioSource>();
        playerVoxAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        swordSwings = new AudioClip[] {swordSwingOne, swordSwingTwo, swordSwingThree, swordSwingFour};
        lastSwing = Time.time;
    }

    public void PlayMovementClip(AudioClip clip)
    {
        playerMovementAudioSource.clip = clip;
        playerMovementAudioSource.Play();
    }

    public void PlayVoxClip(AudioClip clip)
    {
        playerVoxAudioSource.clip = clip;
        playerVoxAudioSource.Play();
    }

    public void playLeftFootstepSound()
    {
        PlayMovementClip(leftFootstep);
    }

    public void playRightFootstepSound()
    {
        PlayMovementClip(rightFootstep);
    }

    public void playSwordSwingSound()
    {
        PlayMovementClip(swordSwings[swingCount % 4]);
        swingCount++;
        lastSwing = Time.time;
    }

    public void playPlayerDamageSound()
    {
        PlayVoxClip(playerDamage);
    }

    private void Update()
    {
        if (Time.time - lastSwing > 4.0f)
        {
            swingCount = 0;
        }
    }

}