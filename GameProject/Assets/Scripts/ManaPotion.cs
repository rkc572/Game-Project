using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    public override void Consume()
    {
        Player.Instance.ModifyManaByAmount(500.0f);
        quantity--;
    }
}
