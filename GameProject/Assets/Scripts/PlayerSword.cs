using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;

public class PlayerSword : PlayerItem
{
    public PlayerSword(Player player) : base(player)
    {
    }

    public override void Action()
    {
        float colliderYoffset = 0.09f;
        Vector3 attackOffset;

        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);

        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.09f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictPhysicalDamage(25.0f * player.properties.physicalAttackMultiplier * player.properties.attackMultiplier);
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }
        player.animator.SetTrigger("SwingSword");
    }

    protected override void EarthAction()
    {
        float colliderYoffset = 0.09f;
        Vector3 attackOffset;

        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);

        // create water swing prefab
        var angle = Mathf.Atan2(player.animator.GetFloat("VerticalMagnitude"), player.animator.GetFloat("HorizontalMagnitude")) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Earth Swing.prefab", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);

        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.09f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictElementalDamage(25.0f * player.properties.elementalAttackMultiplier * player.properties.attackMultiplier);
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                //stun mob
                enemyMob.propertiesManager.ToggleEffectState(new StunnedEffect(enemyMob.propertiesManager, 2.0f));
                break;
            }
        }
        player.sounds.PlayEarthSFX();
        player.animator.SetTrigger("SwingSword");
    }

    protected override void FireAction()
    {
        float colliderYoffset = 0.09f;
        Vector3 attackOffset;
        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);

        // create fire swing prefab
        var angle = Mathf.Atan2(player.animator.GetFloat("VerticalMagnitude"), player.animator.GetFloat("HorizontalMagnitude")) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);


        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.09f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictElementalDamage(25.0f * player.properties.elementalAttackMultiplier * player.properties.attackMultiplier);
                // set mob on fire
                enemyMob.propertiesManager.ToggleEffectState(new BurningEffect(enemyMob.propertiesManager, 1.0f, 4.0f));
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }

        player.sounds.PlayFireSFX();
        player.animator.SetTrigger("SwingSword");
    }

    protected override void WaterAction()
    {
        float colliderYoffset = 0.09f;
        Vector3 attackOffset;
        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);

        // create water swing prefab
        var angle = Mathf.Atan2(player.animator.GetFloat("VerticalMagnitude"), player.animator.GetFloat("HorizontalMagnitude")) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Water Swing.prefab", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);

        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.09f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                float damageDealt = 25.0f * player.properties.physicalAttackMultiplier * player.properties.attackMultiplier;
                enemyMob.propertiesManager.InflictElementalDamage(damageDealt);
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));

                // Heals player for a small % of damage dealt
                // 20 percent
                player.properties.propertiesManager.ModifyHealthByAmount(damageDealt * 0.2f);

                break;
            }
        }
        player.sounds.PlayWaterSFX();
        player.animator.SetTrigger("SwingSword");
    }

    protected override void WindAction()
    {
        float colliderYoffset = 0.09f;
        Vector3 attackOffset;
        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);

        // create Wind swing prefab
        var angle = Mathf.Atan2(player.animator.GetFloat("VerticalMagnitude"), player.animator.GetFloat("HorizontalMagnitude")) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Wind Swing.prefab", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);

        //increased range
        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.15f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                enemyMob.GetComponentInParent<Animator>().SetTrigger("TookDamage");
                enemyMob.propertiesManager.InflictElementalDamage(25.0f * player.properties.elementalAttackMultiplier * player.properties.attackMultiplier);

                //if closer to player deal more damage
                Vector2 mobPosition = enemyMob.transform.position;
                Vector2 playerPosition = player.transform.position;
                float distanceFromPlayer = Vector2.Distance(playerPosition, mobPosition);
                //Debug.Log(distanceFromPlayer);
                if (distanceFromPlayer > 0.3f && distanceFromPlayer < 0.4f)
                {
                    enemyMob.propertiesManager.InflictElementalDamage(10.0f);
                    Debug.Log("hit at the tip with more damage");
                }

                //slightly longer knockback
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.16f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }
        player.sounds.PlayWindSFX();
        player.animator.SetTrigger("SwingSword");
    }
}