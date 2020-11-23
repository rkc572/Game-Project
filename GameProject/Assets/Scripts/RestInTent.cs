using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestInTent : MonoBehaviour
{
    Player player;
    public GameSceneManager gsm;

    void Awake()
    {
        player = Player.Instance;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(gsm.SceneFadeTransition("Forest scene 1", new Vector3(0.66f, -0.6f, 0.0f)));
            player.ModifyHealthByAmount(player.MAX_HEALTH);
            player.ModifyManaByAmount(player.MAX_MANA);
            player.animator.SetFloat("HorizontalMagnitude", 0);
            player.animator.SetFloat("VerticalMagnitude", -1);
        }
    }
}
