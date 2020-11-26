using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    public BlockedWalls top;
    public BlockedWalls bot;
    
    void Update()
    {
        if (AllEnemyDead())
        {
             top.trigger = true;
        }
    }

    bool AllEnemyDead()
    {
        bool allDead = true;

        foreach (Enemy enemy in FindObjectsOfType(typeof(Enemy)))
        {
            if (enemy.health > 0)
            {
                return false;
            }
        }

        return allDead;
    }
}
