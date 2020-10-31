using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Mob properties;
    public Animator animator;
    public PlayerItem sword;
    public PlayerItem selectedArtifact = null;

    public float gold = 0.0f;

    List<PlayerItem> artifacts;
    // List<Potions> potions;

    // Detects if any animation is playing except walking and idle
    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput");
    }

    public bool PlayerTakingDamage()
    {
        return animator.GetCurrentAnimatorStateInfo(1).IsTag("hurt");
    }

    private void Awake()
    {
        sword = new PlayerSword(this);

        DontDestroyOnLoad(gameObject);

        // For presentation use only, remove in production
        // properties.propertiesManager.ToggleEffectState(new SlowedEffect(properties.propertiesManager, 10.5f));
    }

    private void Update()
    {
        if (properties.health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (PlayerTakingDamage())
        {
            player.tag = "PlayerHurt";
        }
        else
        {
            player.tag = "Player";
        }
    }
}
