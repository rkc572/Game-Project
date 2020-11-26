using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoom2 : MonoBehaviour
{
    public BlockedWalls left;
    public BlockedWalls right;
    public int index;

    void Update()
    {

        if (Player.Instance.hasUpgrades[index])
        {
            left.hasUpgrade = true;
            right.hasUpgrade = true;
        }

        if (Player.Instance.hasItems[index])
        {
            right.trigger = true;
        }
    }
}
