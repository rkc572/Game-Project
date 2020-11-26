using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    void Update()
    {
        if (Player.Instance.hasUpgrades[0] && 
            Player.Instance.hasUpgrades[1] && 
            Player.Instance.hasUpgrades[2] && 
            Player.Instance.hasUpgrades[3])
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
