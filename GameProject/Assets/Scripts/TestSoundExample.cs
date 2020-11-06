using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundExample : MonoBehaviour
{
    public AudioClip BattleMusic;

    void Start()
    {
        if (AudioManager.Instance.musicSource.clip != BattleMusic)
        {
            AudioManager.Instance.musicSource.loop = true;
            AudioManager.Instance.PlayMusic(BattleMusic);
        }
    }
}
