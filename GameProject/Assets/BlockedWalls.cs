using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedWalls : MonoBehaviour
{
    public bool entranceLeft;
    public bool entranceRight;
    public bool entranceBot;
    public bool trigger;
    public bool hasUpgrade;

    
    void Update()
    {
        if (entranceLeft)
        {
            if (Player.Instance.transform.position.x > transform.position.x + 0.075f && !hasUpgrade) // Close passage when entering from left
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if (entranceRight)
        {
            if (Player.Instance.transform.position.x < transform.position.x - 0.075f && !hasUpgrade) // Close passage when entering from right
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else if (entranceBot)
        {
            if (Player.Instance.transform.position.y > transform.position.y + 0.65f && !hasUpgrade) // Close passage when entering from right
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        if (trigger || hasUpgrade) // Open passage when room is complete (trigger) or once the item for the sub dungeon has been acquired (hasItem)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
