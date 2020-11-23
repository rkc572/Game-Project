using UnityEngine;
//Knockback effect
public class RepulsedEffect : EffectState
{
    float effectDuration;

    Vector2 direction;
    float strength;
    bool appliedEffect = false;

    public RepulsedEffect(Mob mob, float effectDuration, Vector2 direction, float strength) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.direction = direction;
        this.strength = strength;
    }

    protected override void Effect()
    {
        mob.movementController.StopMoving();
        mob.inputController.detectInput = false;

        //set mob velocity towards repulsion direction with magnitude of strength
        mob.rigidBody.velocity = direction * strength;
        mob.animator.SetFloat("HorizontalMagnitude", -direction.x);
        mob.animator.SetFloat("VerticalMagnitude", -direction.y);
    }

    public override void ApplyEffect()
    {
        if (!appliedEffect)
        {
            Effect();
            appliedEffect = true;
        }

        // Reset after effect duration has run out
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            mob.rigidBody.velocity = Vector2.zero;
            complete = true;
            mob.inputController.detectInput = true;
        }
    }
}
