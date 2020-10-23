using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
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
        swordSwings = new AudioClip[] {swordSwingOne, swordSwingTwo, swordSwingThree, swordSwingFour};
        lastSwing = Time.time;
    }

    public void playLeftFootstepSound()
    {
        AudioManager.Instance.Play(leftFootstep);
    }

    public void playRightFootstepSound()
    {
        AudioManager.Instance.Play(rightFootstep);
    }

    public void playSwordSwingSound()
    {
        AudioManager.Instance.Play(swordSwings[swingCount % 4]);
        swingCount++;
        lastSwing = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSwing > 4.0f)
        {
            swingCount = 0;
        }
    }

}