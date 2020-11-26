using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoom3 : MonoBehaviour
{
    public BlockedWalls entrance;
    public int index;

    void Update()
    {

        if (Player.Instance.hasUpgrades[index])
        {
            entrance.hasUpgrade = true;
        }
    }
}
