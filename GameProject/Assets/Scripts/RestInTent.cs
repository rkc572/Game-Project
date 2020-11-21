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
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(gsm.SceneFadeTransition("Forest scene 1", new Vector3(0.66f, -0.6f, 0.0f)));
            player.properties.propertiesManager.ModifyHealthByAmount(10000f);
            player.properties.propertiesManager.ModifyManaByAmount(10000f);
            player.animator.SetFloat("HorizontalMagnitude", 0);
            player.animator.SetFloat("VerticalMagnitude", -1);
        }
    }
}
