using UnityEngine;
//Knockback effect
public class RepulsedEffect : EffectState
{
    float effectDuration;

    Vector2 direction;
    float strength;

    public RepulsedEffect(PropertiesManager propertiesManager, float effectDuration, Vector2 direction, float strength) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;

        var scaledDirection = direction;

        if (scaledDirection.y > 0)
        {
            scaledDirection.y = 1;
        }
        else if (scaledDirection.y < 0)
        {
            scaledDirection.y = -1;
        }

        if (scaledDirection.x > 0)
        {
            scaledDirection.x = 1;
        }
        else if (scaledDirection.x < 0)
        {
            scaledDirection.x = -1;
        }

        this.direction = scaledDirection;
        this.strength = strength;
    }

    protected override void Effect()
    {
        //set player velocity towards repulsion direction with magnitude of strength
        propertiesManager.mob.rigidBody.velocity = direction * strength;
        propertiesManager.GetComponentInParent<Animator>().SetFloat("HorizontalMagnitude", -direction.x);
        propertiesManager.GetComponentInParent<Animator>().SetFloat("VerticalMagnitude", -direction.y);
    }

    public override void ApplyEffect()
    {
        Effect();

        // Reset after effect duration has run out
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            propertiesManager.mob.rigidBody.velocity = Vector2.zero;
            complete = true;
        }
    }
}
