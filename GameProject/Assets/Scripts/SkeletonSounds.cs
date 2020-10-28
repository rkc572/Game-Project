using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSounds : MonoBehaviour
{
    public AudioClip skeletonDamage;

    public AudioClip attack;

    public void playSkeletonDamageSound()
    {
        AudioManager.Instance.Play(skeletonDamage);
    }

    public void playSkeletonAttackSound()
    {
        AudioManager.Instance.Play(attack);
    }

}
