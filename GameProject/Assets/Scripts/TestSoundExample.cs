using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundExample : MonoBehaviour
{
    public AudioClip BattleMusic;

    void Start()
    {
        AudioManager.Instance.PlayMusic(BattleMusic);
    }
}
