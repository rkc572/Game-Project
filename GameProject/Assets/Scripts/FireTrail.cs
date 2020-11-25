using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
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
            enemy.InflictElementalDamage(5.0f);
            enemy.animator.SetTrigger("TookDamage");
            enemy.ToggleEffectState(new BurningEffect(enemy, 1.0f, 6.0f, 5.0f));
        }
    }
}
