using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCircleAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.parent.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponentInParent<Player>();
        var skeleton = collider.GetComponentInParent<Skeleton>();

        if (collider.CompareTag("Player") && !player.isEthereal)
        {
            // slow down player
            player.ToggleEffectState(new SlowedEffect(player, 10.0f, 0.5f));

            List<EffectState> randomEffects = new List<EffectState>{
                    new BurningEffect(player, 0.8f, 6.0f, 5.0f),
                    new JinxedEffect(player, 4.0f, 0.5f),
                    new WeakenedEffect(player, 4.0f, 0.5f),
                    new VulnerableEffect(player, 4.0f, 1.5f)
                };

            player.ToggleEffectState(randomEffects[Random.Range(0, randomEffects.Count)]);

            // deal damage
            player.InflictElementalDamage(30.0f);
            player.animator.SetTrigger("PlayerHurt");
        }
        
        if (skeleton != null && ((SkeletonMovementController)skeleton.movementController).movementMode == SkeletonMovementController.MovementMode.Disabled)
        {
            skeleton.animator.SetTrigger("Revive");
            skeleton.ToggleEffectState(new StrengthenedEffect(skeleton, 10.0f, 1.5f));
        }
    }

}
