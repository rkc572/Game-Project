using UnityEngine;

public class BurningEffect : EffectState
{
    float effectDuration;
    float lastInflictionTime = 0.0f;
    float actionInterval;
    bool particlesActive = false;
    GameObject fireParticles;

    public BurningEffect(PropertiesManager propertiesManager, float actionInterval, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.actionInterval = actionInterval;
    }

    protected override void Effect()
    {
        Debug.Log($"im burning at {Time.time}");
        propertiesManager.InflictElementalDamage(10.0f);
        propertiesManager.GetComponentInParent<Animator>().SetTrigger("TookDamage");
        propertiesManager.GetComponentInParent<Animator>().SetTrigger("PlayerHurt");
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
            var fireParticlesPrefab = (GameObject)Resources.Load("prefabs/MobOnFire", typeof(GameObject));
            fireParticles = GameObject.Instantiate(fireParticlesPrefab, Vector3.zero, Quaternion.identity);
            fireParticles.transform.position = Vector3.zero;
            fireParticles.transform.SetParent(propertiesManager.mob.transform, false);
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
            GameObject.Destroy(fireParticles);
            Debug.Log($"im done burning at {Time.time}");
        }
    }
}