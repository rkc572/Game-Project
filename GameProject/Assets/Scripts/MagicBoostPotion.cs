using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoostPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ToggleEffectState(new EnchantedEffect(Player.Instance, 10.0f, Player.Instance.elementalAttackMultiplier * 1.5f));
        quantity--;
    }
}
