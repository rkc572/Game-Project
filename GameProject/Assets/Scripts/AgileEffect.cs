using UnityEngine;

//This effect enhances speed
public class AgileEffect : EffectState
{
    //How long effect lasts
    float effectDuration;

    //Hold previous speed value
    float previousSpeed;

    //Get new speed value
    float newSpeed;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject agileParticles;

    public AgileEffect(Mob mob, float effectDuration, float newSpeed) : base(mob)
    {
        //Get duration/new speed
        this.effectDuration = effectDuration;
        this.newSpeed = newSpeed;
        previousSpeed = mob.speed;
    }

    protected override void Effect()
    {
        Debug.Log($"I'm faster! {Time.time}");
        //Set new speed
        mob.SetMobSpeed(newSpeed);
    }

    //Apply
    public override void ApplyEffect()
    {
        // Apply Effect once
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }
        if (!particlesActive)
        {
            var agileParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsAgile", typeof(GameObject));
            agileParticles = GameObject.Instantiate(agileParticlesPrefab, Vector3.zero, Quaternion.identity);
            agileParticles.transform.position = Vector3.zero;
            agileParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        // Reset after effect duration has run out
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(agileParticles);
            mob.SetMobSpeed(previousSpeed);
            Debug.Log("I'm no longer fast!");
        }
    }
}
