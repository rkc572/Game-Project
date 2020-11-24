using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ToggleEffectState(new RegeneratingEffect(Player.Instance, 1.0f, 15.0f, 5.0f));
        quantity--;
    }
}
