using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoom3 : MonoBehaviour
{
    public BlockedWalls left;
    public int index;

    void Update()
    {

        if (Player.Instance.hasUpgrades[index])
        {
            left.hasUpgrade = true;
        }
    }
}
