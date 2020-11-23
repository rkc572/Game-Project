using UnityEngine;
//This effect decreases physical damage taken multipler - Tank Effect
public class FortifiedEffect : EffectState
{
    float effectDuration;
    float previousPhysicalDamageTakenMultiplier;
    float newPhysicalDamageTakenMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject fortifiedParticles;

<<<<<<< HEAD
    public FortifiedEffect(PropertiesManager propertiesManager, float effectDuration, float newPhysicalDamageTakenMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
        previousPhysicalDamageTakenMultiplier = propertiesManager.mob.physicalDamageTakenMultiplier;
=======
    public FortifiedEffect(Mob mob, float effectDuration, float newPhysicalDamageTakenMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
        previousPhysicalDamageTakenMultiplier = mob.physicalDamageTakenMultiplier;
>>>>>>> miguel-game-project-v2
    }

    protected override void Effect()
    {
<<<<<<< HEAD
        propertiesManager.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);
=======
        mob.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);
>>>>>>> miguel-game-project-v2
        Debug.Log("I am fortified - less phys.dmg taken");
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
            var fortifiedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsFortified", typeof(GameObject));
            fortifiedParticles = GameObject.Instantiate(fortifiedParticlesPrefab, Vector3.zero, Quaternion.identity);
            fortifiedParticles.transform.position = Vector3.zero;
<<<<<<< HEAD
            fortifiedParticles.transform.SetParent(propertiesManager.mob.transform, false);
=======
            fortifiedParticles.transform.SetParent(mob.transform, false);
>>>>>>> miguel-game-project-v2
            particlesActive = true;
        }

        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(fortifiedParticles);
<<<<<<< HEAD
            propertiesManager.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
=======
            mob.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
>>>>>>> miguel-game-project-v2
            Debug.Log("I am no longer fortified");
            effectApplied = false;
        }
    }
    
}
