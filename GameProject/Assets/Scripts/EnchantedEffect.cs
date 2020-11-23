using UnityEngine;
//This effect increases Elemental Attack Multiplier
public class EnchantedEffect : EffectState
{
    float effectDuration;
    float previousElementalAttackMultiplier;
    float newElementalAttackMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject enchantedParticles;


    public EnchantedEffect(Mob mob, float effectDuration, float newElementalAttackMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newElementalAttackMultiplier = newElementalAttackMultiplier;
        previousElementalAttackMultiplier = mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I am enchanted!");
        mob.SetElementalAttackMultiplier(newElementalAttackMultiplier);
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
            var enchantedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsEnchanted", typeof(GameObject));
            enchantedParticles = GameObject.Instantiate(enchantedParticlesPrefab, Vector3.zero, Quaternion.identity);
            enchantedParticles.transform.position = Vector3.zero;
            enchantedParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(enchantedParticles);
            mob.SetElementalAttackMultiplier(previousElementalAttackMultiplier);
            Debug.Log("I am no longer enchanted");
            effectApplied = false;
        }
    }
   
}
