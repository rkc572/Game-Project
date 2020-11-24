using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ModifyHealthByAmount(500.0f);
        quantity--;
    }
}
