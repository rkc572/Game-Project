using UnityEngine;
//This effect decreases speed
public class SlowedEffect : EffectState
{
    float effectDuration;

    float previousSpeed;
    float newSpeed;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject slowedParticles;

    public SlowedEffect(Mob mob, float effectDuration, float newSpeed) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newSpeed = newSpeed;
        previousSpeed = mob.speed;
    }
    
    protected override void Effect()
    {
        Debug.Log($"I'm slowed down! {Time.time}");
        mob.SetMobSpeed(newSpeed);
    }
    
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
            var slowedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsSlowed", typeof(GameObject));
            slowedParticles = GameObject.Instantiate(slowedParticlesPrefab, Vector3.zero, Quaternion.identity);
            slowedParticles.transform.position = Vector3.zero;
            slowedParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        // Reset after effect duration has run out
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(slowedParticles);
            mob.SetMobSpeed(previousSpeed);
            Debug.Log("I'm no longer slowed!");
        }
    }
}
