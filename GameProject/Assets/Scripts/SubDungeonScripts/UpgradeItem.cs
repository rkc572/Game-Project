using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    public Collider2D trigger;
    public GameObject upgradeUI;
    public int index;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            upgradeUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (AllEnemyDead() && !Player.Instance.hasUpgrades[index])
        {
            trigger.enabled = true;
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
