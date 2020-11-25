using UnityEngine;
//This effect freezes you and increases physical damage taken
public class FrozenEffect : EffectState
{
    float effectDuration;
    float previousSpeed, previousPhysicalDamageTakenMultiplier;
    float frozenSpeed = 0.0f;
    float newPhysicalDamageTakenMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject frozenParticles;

    public FrozenEffect(Mob mob, float effectDuration, float newPhysicalDamageTakenMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
        previousSpeed = mob.speed;
        previousPhysicalDamageTakenMultiplier = mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        mob.SetMobSpeed(frozenSpeed);
        mob.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);


        mob.movementController.StopMoving();
        mob.animator.SetBool("Moving", false);
        mob.inputController.detectInput = false;
        mob.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;


        Debug.Log("I am frozen and take more damage!");
    }

    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }
        if (!particlesActive)
        {
            var frozenParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsFrozen", typeof(GameObject));
            frozenParticles = GameObject.Instantiate(frozenParticlesPrefab, Vector3.zero, Quaternion.identity);
            frozenParticles.transform.position = Vector3.zero;
            frozenParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(frozenParticles);
            mob.SetMobSpeed(previousSpeed);
            mob.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
            Debug.Log("I am no longer frozen");
            effectApplied = false;
            mob.inputController.detectInput = true;
            mob.inputController.detectMovementInput = true;
            mob.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }




}
