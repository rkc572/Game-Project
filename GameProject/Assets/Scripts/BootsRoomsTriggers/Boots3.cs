using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots3 : MonoBehaviour
{
    public BlockedWalls left;
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
            }
        }
    }
}
