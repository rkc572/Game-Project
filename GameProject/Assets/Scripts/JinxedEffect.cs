using UnityEngine;
//This effect decreases Elemental Attack Multiplier
public class JinxedEffect : EffectState
{
    float effectDuration;
    float previousElementalAttackMultiplier;
    float newElementalAttackMultiplier;

    bool effectApplied = false;
    bool particlesActive = false;
    GameObject jinxedParticles;

    public JinxedEffect(Mob mob, float effectDuration, float newElementalAttackMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newElementalAttackMultiplier = newElementalAttackMultiplier;
        previousElementalAttackMultiplier = mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        mob.SetElementalAttackMultiplier(newElementalAttackMultiplier);
        Debug.Log("I am jinxed - Elemental Attack Multiplier decreased");

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
            var jinxedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsJinxed", typeof(GameObject));
            jinxedParticles = GameObject.Instantiate(jinxedParticlesPrefab, Vector3.zero, Quaternion.identity);
            jinxedParticles.transform.position = Vector3.zero;
            jinxedParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(jinxedParticles);
            mob.SetElementalAttackMultiplier(previousElementalAttackMultiplier);
            Debug.Log("I am no longer jinxed");
            effectApplied = false;
        }
    }

}
