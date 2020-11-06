using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject player;
    public PlayerInputController playerInputController;
    public Mob properties;
    public Animator animator;
    public PlayerItem sword;
    public PlayerItem selectedArtifact = null;
    public PlayerSounds sounds;

    public float lastRecordedHealth;
    public float lastRecordedMana;
    public Vector2 lastRecordedPosition;

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
        sword.elementalAttribute = ElementalAttribute.Fire;

        lastRecordedHealth = properties.health;
        lastRecordedMana = properties.mana;
        lastRecordedPosition = transform.parent.position;

        // For presentation use only, remove in production
        // properties.propertiesManager.ToggleEffectState(new SlowedEffect(properties.propertiesManager, 10.5f));
        //properties.propertiesManager.ToggleEffectState(new WeakenedEffect(properties.propertiesManager, 10.5f));
        //operties.propertiesManager.ToggleEffectState(new StunnedEffect(properties.propertiesManager, 10.5f));
    }

    private void Update()
    {
        if (properties.health <= 0 && !GameSceneManager.Instance.deathSceneActive)
        {
            StartCoroutine(GameSceneManager.Instance.PlayerDied(this));
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

    void OnDrawGizmos()
    {

        float colliderYoffset = 0.09f;
        Vector3 attackOffset;

        attackOffset = new Vector3(animator.GetFloat("HorizontalMagnitude") * 0.13f, animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);
        Gizmos.DrawWireSphere(properties.rigidBody.transform.position + attackOffset, 0.09f);

        // up Gizmos.DrawWireSphere(properties.rigidBody.transform.position + new Vector3(0.0f, 0.21f, 0.0f), 0.09f);
        // down Gizmos.DrawWireSphere(properties.rigidBody.transform.position + new Vector3(0.0f, -0.03f, 0.0f), 0.09f);
        // right Gizmos.DrawWireSphere(properties.rigidBody.transform.position + new Vector3(0.12f, 0.09f, 0.0f), 0.09f);
        // left Gizmos.DrawWireSphere(properties.rigidBody.transform.position + new Vector3(0.12f, 0.09f, 0.0f), 0.09f);
    }
}
