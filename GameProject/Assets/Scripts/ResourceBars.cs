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
        player = FindObjectOfType<Player>();
        if (resourceType)
            bar.maxValue = player.properties.MAX_HEALTH;
        else
            bar.maxValue = player.properties.MAX_MANA;
    }

    void Update ()
    {
        if (resourceType)
            resource = player.GetComponent<Mob>().health;
        else
            resource = player.GetComponent<Mob>().mana;
        bar.value = resource;
    }
}
