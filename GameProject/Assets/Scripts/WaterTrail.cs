using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrail : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.ToggleEffectState(new FrozenEffect(enemy, 3.0f, 1.5f * Player.Instance.elementalAttackMultiplier));
        }
    }
}
