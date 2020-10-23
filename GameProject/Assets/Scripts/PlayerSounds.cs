using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip leftFootstep;
    public AudioClip rightFootstep;
    public AudioClip swordSwing;

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
        AudioManager.Instance.Play(swordSwing);
    }

}