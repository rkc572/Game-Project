using UnityEngine;
//This effect allows regeneration
public class RegeneratingEffect : EffectState
{
    float effectDuration;
    float lastInflictionTime = 0.0f;
    float actionInterval;
    float healthRegenAmount;

    bool particlesActive = false;
    GameObject regenParticles;

    public RegeneratingEffect(Mob mob, float actionInterval, float effectDuration, float healthRegenAmount) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.actionInterval = actionInterval;
        this.healthRegenAmount = healthRegenAmount;
    }

    protected override void Effect()
    {
        Debug.Log($"im healing at {Time.time}");
        mob.ModifyHealthByAmount(healthRegenAmount);
    }

    public override void ApplyEffect()
    {
        // Initialize lastInflictionTime
        if (lastInflictionTime == 0.0f)
        {
            lastInflictionTime = Time.time;
        }

        if (!particlesActive)
        {
            var regenParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsRegenerating", typeof(GameObject));
            regenParticles = GameObject.Instantiate(regenParticlesPrefab, Vector3.zero, Quaternion.identity);
            regenParticles.transform.position = Vector3.zero;
            regenParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        // Check if EffectState is still active
        if (Time.time < effectInitializedTime + effectDuration && !complete)
        {
            // Check if it is within the interval to apply effect
            if (Time.time - lastInflictionTime >= actionInterval)
            {
                // Apply effect
                Effect();

                // Reset LastInflictionTime
                lastInflictionTime = Time.time;
            }
        }
        else
        {
            // Set complete to true if Effect state has run out and was not marked complete
            complete = true;
            GameObject.Destroy(regenParticles);
            Debug.Log($"im done regenerating at {Time.time}");
        }
    }
}