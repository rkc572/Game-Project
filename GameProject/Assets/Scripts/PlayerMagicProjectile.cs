using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicProjectile : MonoBehaviour
{

    public ElementalAttribute elementalAttribute;
    public Rigidbody2D rigidBody;

    void Effect(Enemy enemy)
    {
        enemy.InflictElementalDamage(20.0f * Player.Instance.elementalAttackMultiplier);
        enemy.animator.SetTrigger("TookDamage");

        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                break;
            case ElementalAttribute.EARTH:
                enemy.ToggleEffectState(new StunnedEffect(enemy, 2.0f));
                break;
            case ElementalAttribute.FIRE:
                enemy.ToggleEffectState(new BurningEffect(enemy, 1.0f, 6.0f, 5.0f));
                break;
            case ElementalAttribute.WATER:
                enemy.ToggleEffectState(new FrozenEffect(enemy, 1.5f, 1.5f));
                break;
            case ElementalAttribute.AIR:
                enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.35f, (enemy.transform.position - Player.Instance.transform.position).normalized, 2.0f));
                break;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Player.Instance.isEthereal)
        {
            return;
        }

        if (collider.gameObject.layer == 9) // ENEMY LAYER
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Effect(enemy);
                Player.Instance.playerSounds.PlayMagicImpactSFX();
            }
        }
    }
}
