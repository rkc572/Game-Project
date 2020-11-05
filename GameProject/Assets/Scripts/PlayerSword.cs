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
                enemyMob.propertiesManager.InflictPhysicalDamage(25.0f * player.properties.physicalAttackMultiplier * player.properties.attackMultiplier);
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }
        player.animator.SetTrigger("SwingSword");
    }

    protected override void EarthAction()
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
        Vector3 attackOffset;
        if (player.animator.GetFloat("VerticalMagnitude") <= 0.0f)
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f, 0.0f);
        }
        else
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f - 0.05f, 0.0f);
        }

        // facing up
        if (player.animator.GetFloat("VerticalMagnitude") == 1 && player.animator.GetFloat("HorizontalMagnitude") == 0)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 180);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing down
        else if (player.animator.GetFloat("VerticalMagnitude") == -1 && player.animator.GetFloat("HorizontalMagnitude") == 0)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(0, -0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 0);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing left
        else if (player.animator.GetFloat("VerticalMagnitude") == 0 && player.animator.GetFloat("HorizontalMagnitude") == -1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(-0.1f, 0, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 270);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing right
        else if (player.animator.GetFloat("VerticalMagnitude") == 0 && player.animator.GetFloat("HorizontalMagnitude") == 1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 90);
            Object.Destroy(fireSwingObject, 0.6f);
        }

        // facing up right
        if (player.animator.GetFloat("VerticalMagnitude") == 1 && player.animator.GetFloat("HorizontalMagnitude") == 1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(0.1f, 0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 135);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing down right
        else if (player.animator.GetFloat("VerticalMagnitude") == -1 && player.animator.GetFloat("HorizontalMagnitude") == 1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(0.1f, -0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 45);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing up left
        if (player.animator.GetFloat("VerticalMagnitude") == 1 && player.animator.GetFloat("HorizontalMagnitude") == -1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(-0.1f, 0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 225);
            Object.Destroy(fireSwingObject, 0.6f);
        }
        // facing down left
        else if (player.animator.GetFloat("VerticalMagnitude") == -1 && player.animator.GetFloat("HorizontalMagnitude") == -1)
        {
            var fireSwingPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Swing.prefab", typeof(GameObject));
            var fireSwingObject = GameObject.Instantiate(fireSwingPrefab, player.transform.position + new Vector3(-0.1f, -0.1f, 0), Quaternion.identity);
            fireSwingObject.transform.eulerAngles = new Vector3(fireSwingObject.transform.position.x, fireSwingObject.transform.position.y, 315);
            Object.Destroy(fireSwingObject, 0.6f);
        }



        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.15f);
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
        Vector3 attackOffset;
        if (player.animator.GetFloat("VerticalMagnitude") <= 0.0f)
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f, 0.0f);
        }
        else
        {
            attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.1f, player.animator.GetFloat("VerticalMagnitude") * 0.15f - 0.05f, 0.0f);
        }

        //increased range
        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.25f);
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
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.12f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }
        player.sounds.PlayWindSFX();
        player.animator.SetTrigger("SwingSword");
    }
}