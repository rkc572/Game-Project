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

        if (player != null)
        {
            // slow down player
            player.properties.propertiesManager.ToggleEffectState(new SlowedEffect(player.properties.propertiesManager, 10.0f, 0.5f));

            List<EffectState> randomEffects = new List<EffectState>{
                    new BurningEffect(player.properties.propertiesManager, 0.8f, 6.0f, 10.0f),
                    new FrozenEffect(player.properties.propertiesManager, 4.0f, 1.5f),
                    new JinxedEffect(player.properties.propertiesManager, 4.0f, 0.5f),
                    new WeakenedEffect(player.properties.propertiesManager, 4.0f, 0.5f),
                    new VulnerableEffect(player.properties.propertiesManager, 4.0f, 1.5f),
                    new StunnedEffect(player.properties.propertiesManager, 4.0f),
                };

            player.properties.propertiesManager.ToggleEffectState(randomEffects[Random.Range(0, randomEffects.Count)]);

            // deal damage
            player.properties.propertiesManager.InflictElementalDamage(30.0f);
            player.animator.SetTrigger("PlayerHurt");
        }
        else if (skeleton != null && skeleton.movementMode == Skeleton.MovementMode.Disabled)
        {
            skeleton.animator.SetTrigger("Revive");
            skeleton.properties.propertiesManager.ToggleEffectState(new StrengthenedEffect(skeleton.properties.propertiesManager, 10.0f, 1.5f));
        }
    }

}
