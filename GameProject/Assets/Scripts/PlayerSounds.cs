﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource playerMovementAudioSource;
    public AudioSource playerAttackAudioSource;
    public AudioSource playerElementalAttackAudioSource;
    public AudioSource playerVoxAudioSource;

    public AudioClip playerDamage;

    public AudioClip leftFootstep;
    public AudioClip rightFootstep;

    public AudioClip swordSwingOne;
    public AudioClip swordSwingTwo;
    public AudioClip swordSwingThree;
    public AudioClip swordSwingFour;

    public AudioClip shieldSFX;

    public AudioClip waterAttackSFX;
    public AudioClip fireAttackSFX;
    public AudioClip earthAttackSFX;
    public AudioClip windAttackSFX;

    public AudioClip goldSFX;
    public AudioClip healthSFX;
    public AudioClip manaSFX;


    public AudioClip magicLaunchSFX;
    public AudioClip magicImpactSFX;
    public AudioClip pendantActivateSFX;
    public AudioClip pendantDectivateSFX;

    public AudioClip invalidInputSFX;

    AudioClip[] swordSwings;

    int swingCount = 0;
    float lastSwing;

    private void Start()
    {
        playerMovementAudioSource = gameObject.AddComponent<AudioSource>();
        playerMovementAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        playerAttackAudioSource = gameObject.AddComponent<AudioSource>();
        playerAttackAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        playerElementalAttackAudioSource = gameObject.AddComponent<AudioSource>();
        playerElementalAttackAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        playerVoxAudioSource = gameObject.AddComponent<AudioSource>();
        playerVoxAudioSource.outputAudioMixerGroup = AudioManager.Instance.audioMixer.FindMatchingGroups("FX")[0];
        swordSwings = new AudioClip[] { swordSwingOne, swordSwingTwo, swordSwingThree, swordSwingFour };
        lastSwing = Time.time;
    }

    public void PlayMovementClip(AudioClip clip)
    {
        playerMovementAudioSource.clip = clip;
        playerMovementAudioSource.Play();
    }

    public void PlayAttackClip(AudioClip clip)
    {
        playerAttackAudioSource.clip = clip;
        playerAttackAudioSource.Play();
    }

    public void PlayElementalAttackClip(AudioClip clip)
    {
        playerElementalAttackAudioSource.clip = clip;
        playerElementalAttackAudioSource.Play();
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
        PlayAttackClip(swordSwings[swingCount % 4]);
        swingCount++;
        lastSwing = Time.time;
    }

    public void playPlayerDamageSound()
    {
        PlayVoxClip(playerDamage);
    }

    public void PlayWaterSFX()
    {
        PlayElementalAttackClip(waterAttackSFX);
    }

    public void PlayFireSFX()
    {
        PlayElementalAttackClip(fireAttackSFX);
    }

    public void PlayEarthSFX()
    {
        PlayElementalAttackClip(earthAttackSFX);
    }

    public void PlayWindSFX()
    {
        PlayElementalAttackClip(windAttackSFX);
    }

    public void PlayShieldSFX()
    {
        PlayAttackClip(shieldSFX);
    }

    public void PlayGoldPickupSFX()
    {
        PlayVoxClip(goldSFX);
    }

    public void PlayHealthPickupSFX()
    {
        PlayVoxClip(healthSFX);
    }

    public void PlayManaPickupSFX()
    {
        PlayVoxClip(manaSFX);
    }

    public void PlayMagicStaffSFX()
    {
        PlayAttackClip(magicLaunchSFX);
    }

    public void PlayMagicImpactSFX()
    {
        PlayAttackClip(magicImpactSFX);
    }

    public void PlayPendantActivationSFX()
    {
        PlayAttackClip(pendantActivateSFX);
    }

    public void PlayPendantDeactivationSFX()
    {
        PlayAttackClip(pendantDectivateSFX);
    }

    public void PlayInvalidInputSFX()
    {
        PlayVoxClip(invalidInputSFX);
    }

    private void Update()
    {
        if (Time.time - lastSwing > 4.0f)
        {
            swingCount = 0;
        }
    }

}