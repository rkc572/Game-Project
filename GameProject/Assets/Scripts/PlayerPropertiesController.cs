using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertiesController : MonoBehaviour
{
    public Player player;

    float MAX_PLAYER_HEALTH = 1000;
    float MAX_PLAYER_MANA = 1000;

    void ModifyPlayerHealth(float amount)
    {
        player.health = Mathf.Clamp(player.health + amount, 0, MAX_PLAYER_HEALTH);
    }

    void ModifyPlayerMana(float amount)
    {
        player.mana = Mathf.Clamp(player.mana + amount, 0, MAX_PLAYER_MANA);
    }

    void ModifyPlayerSpeed(float amount)
    {
        player.speed += amount;
    }

}
