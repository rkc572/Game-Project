using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots2 : MonoBehaviour
{
    public BlockedWalls left;
    public BlockedWalls right;
    int index;

    void Start()
    {
        index = Player.Instance.artifacts.IndexOf(Player.Instance.playerDashBoots);
    }

    void Update()
    {
        if (index >= 0)
        {
            if (Player.Instance.artifacts[index].elementalAttribute != ElementalAttribute.NONE)
            {
                left.hasItem = true;
                right.hasItem = true;
            }
        }

        if (Player.Instance.artifacts.Contains(Player.Instance.playerDashBoots))
        {
            right.trigger = true;
        }
    }
}
