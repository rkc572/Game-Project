using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    public GameObject upgradeUI;
    public GameObject entity;
    public int index;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            upgradeUI.SetActive(true);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Player.Instance.inputController.detectInput = false;
            Player.Instance.movementController.StopMoving();
        }
    }

    void Update()
    {
        if (AllEnemyDead() && !Player.Instance.hasUpgrades[index] && Player.Instance.health > 0)
        {
            entity.SetActive(true);
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
