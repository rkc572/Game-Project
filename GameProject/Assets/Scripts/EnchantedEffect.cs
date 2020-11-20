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


    public EnchantedEffect(PropertiesManager propertiesManager, float effectDuration, float newElementalAttackMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newElementalAttackMultiplier = newElementalAttackMultiplier;
        previousElementalAttackMultiplier = propertiesManager.mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I am enchanted!");
        propertiesManager.SetElementalAttackMultiplier(newElementalAttackMultiplier);
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
            enchantedParticles.transform.SetParent(propertiesManager.mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(enchantedParticles);
            propertiesManager.SetElementalAttackMultiplier(previousElementalAttackMultiplier);
            Debug.Log("I am no longer enchanted");
            effectApplied = false;
        }
    }
   
}
