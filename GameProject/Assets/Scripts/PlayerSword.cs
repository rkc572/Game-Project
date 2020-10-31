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
        Vector3 attackOffset;
        if (player.animator.GetFloat("VerticalMagnitude") <= 0.0f)
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f, 0.0f);
        }
        else
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f - 0.05f, 0.0f);
        }

        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.15f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictPhysicalDamage(25.0f * player.properties.physicalAttackMultiplier);
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 4.0f));
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