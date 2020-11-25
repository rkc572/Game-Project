using UnityEngine;
//This effect stuns/freezes you
public class StunnedEffect : EffectState
{
    float effectDuration;
    float previousSpeed;
    //Stunned Effect - frozen cant move
    bool effectApplied = false;


    public StunnedEffect(Mob mob, float effectDuration) : base(mob)
    {
        this.effectDuration = effectDuration;
        previousSpeed = mob.speed;
    }

    protected override void Effect()
    {
        Debug.Log("I am stunned - can't move");
        mob.movementController.StopMoving();
        mob.animator.SetBool("Moving", false);
        mob.inputController.detectInput = false;
        mob.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            Debug.Log("I am no longer stunned - I can move");
            mob.inputController.detectInput = true;
            mob.inputController.detectMovementInput = true;
            effectApplied = false;
            mob.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
