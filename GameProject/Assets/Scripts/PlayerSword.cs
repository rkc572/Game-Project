﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerItem
{
    public PlayerSword(Player player) : base(player)
    {
    }

    public override void Action()
    {
        var colliders = Physics2D.OverlapCircleAll(player.transform.position, 0.3f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictPhysicalDamage(25.0f * player.properties.physicalAttackMultiplier);
                break;
            }
        }
        player.animator.SetTrigger("SwingSword");
    }

    protected override void EarthAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void WaterAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void WindAction()
    {
        throw new System.NotImplementedException();
    }
}