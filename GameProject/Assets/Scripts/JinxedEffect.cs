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

    public JinxedEffect(PropertiesManager propertiesManager, float effectDuration, float newElementalAttackMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newElementalAttackMultiplier = newElementalAttackMultiplier;
        previousElementalAttackMultiplier = propertiesManager.mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetElementalAttackMultiplier(newElementalAttackMultiplier);
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
            jinxedParticles.transform.SetParent(propertiesManager.mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(jinxedParticles);
            propertiesManager.SetElementalAttackMultiplier(previousElementalAttackMultiplier);
            Debug.Log("I am no longer jinxed");
            effectApplied = false;
        }
    }

}
