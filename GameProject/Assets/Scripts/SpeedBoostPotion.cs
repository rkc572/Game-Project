using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ToggleEffectState(new AgileEffect(Player.Instance, 10.0f, Player.Instance.speed * 1.5f));
        quantity--;
    }
}
