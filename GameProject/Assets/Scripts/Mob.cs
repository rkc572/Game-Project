using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public PropertiesManager propertiesManager;

    [Header("Stats")]
    public float MAX_HEALTH = 1000;
    public float MAX_MANA = 1000;
    public float MAX_SPEED = 1.0f;

    public float health = 1000;
    public float mana = 1000;
    public float speed = 1.0f;

    public float attackMultiplier = 1f;
    public float physicalAttackMultiplier = 1f;
    public float elementalAttackMultiplier = 1f;
    
    public float damageTakenMultiplier = 1f;
    public float physicalDamageTakenMultiplier = 1f;
    public float elementalDamageTakenMultiplier = 1f;

    public List<EffectState> effectStates = new List<EffectState>();

    private void Awake()
    {
        // Can test effect states like this
        // effectStates.Add(new BurningEffect(propertiesManager, 1.0f, 10.5f));
        effectStates.Add(new BurningEffect(propertiesManager, 1.0f, 10.5f));
    }
}
