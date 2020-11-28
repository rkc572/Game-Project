using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ToggleEffectState(new StrengthenedEffect(Player.Instance, 10.0f, Player.Instance.physicalAttackMultiplier * 1.5f));
        quantity--;
    }
}
