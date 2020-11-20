using UnityEngine;

//This effect lowers Physical Attack Multiplier
public class WeakenedEffect : EffectState
{
    
    float effectDuration;

    float previousDamageMultiplier;
    //Halves damage
    float newDamageMultiplier;

    bool effectApplied = false;

    bool particlesActive;
    GameObject weakenedParticles;

    public WeakenedEffect(PropertiesManager propertiesManager, float effectDuration, float newDamageMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newDamageMultiplier = newDamageMultiplier;
        previousDamageMultiplier = propertiesManager.mob.physicalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I'm weakened!");
        propertiesManager.SetPhysicalAttackMultiplier(newDamageMultiplier);
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
            var weakenedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsWeakened", typeof(GameObject));
            weakenedParticles = GameObject.Instantiate(weakenedParticlesPrefab, Vector3.zero, Quaternion.identity);
            weakenedParticles.transform.position = Vector3.zero;
            weakenedParticles.transform.SetParent(propertiesManager.mob.transform, false);
            particlesActive = true;
        }

        //After effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(weakenedParticles);
            propertiesManager.SetPhysicalAttackMultiplier(previousDamageMultiplier);
            Debug.Log("I'm not weakened!");
            effectApplied = false;
        }
    }

   
}
