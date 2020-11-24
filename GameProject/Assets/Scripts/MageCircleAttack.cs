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
        var player = collider.GetComponent<Player>();
        var skeleton = collider.GetComponent<Skeleton>();
        /*
        if (player != null)
        {
            // slow down player
            player.ToggleEffectState(new SlowedEffect(player, 10.0f, 0.5f));

            List<EffectState> randomEffects = new List<EffectState>{
                    new BurningEffect(player, 0.8f, 6.0f, 5.0f),
                    new FrozenEffect(player, 4.0f, 1.5f),
                    new JinxedEffect(player, 4.0f, 0.5f),
                    new WeakenedEffect(player, 4.0f, 0.5f),
                    new VulnerableEffect(player, 4.0f, 1.5f),
                    new StunnedEffect(player, 4.0f)
                };

            player.ToggleEffectState(randomEffects[Random.Range(0, randomEffects.Count)]);

            // deal damage
            player.InflictElementalDamage(30.0f);
            player.animator.SetTrigger("PlayerHurt");
        }
        else if (skeleton != null && skeleton.movementMode == Skeleton.MovementMode.Disabled)
        {
            skeleton.animator.SetTrigger("Revive");
            skeleton.ToggleEffectState(new StrengthenedEffect(skeleton, 10.0f, 1.5f));
        }
        */
    }

}
