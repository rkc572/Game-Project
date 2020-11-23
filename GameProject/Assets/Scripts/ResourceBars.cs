using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBars : MonoBehaviour
{
    public Slider bar;
    [Tooltip("True for health, false for mana")]
    public bool resourceType;
    Player player;
    float resource;

    void Start ()
    {
        player = Player.Instance;
        if (resourceType)
            bar.maxValue = player.MAX_HEALTH;
        else
            bar.maxValue = player.MAX_MANA;
    }

    void Update ()
    {
        if (player == null)
        {
            player = Player.Instance;
        }

        if (resourceType)
            resource = player.health;
        else
            resource = player.mana;
        bar.value = resource;
    }
}
