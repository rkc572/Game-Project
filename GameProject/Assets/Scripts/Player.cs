using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{
    // public Player instance reference variable
    public static Player Instance;

    // PlayerItem variables
    public PlayerItem playerSword, playerShield, playerDashBoots, playerMagicStaff, playerEtherealPendant, selectedArtifact;
    public List<PlayerItem> artifacts = new List<PlayerItem>();

    // potion variables
    public Potion selectedPotion;
    public List<Potion> potions = new List<Potion>();

    // player Ethereal status
    public bool isEthereal = false;

    // player Shielding status
    public bool isBlocking = false;


    public PlayerSounds playerSounds;
    public Vector3 lastRecordedPosition;
    public float gold;
    public float lastRecordedHealth, lastRecordedMana;

    public bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput");
    }

    public bool PlayerTakingDamage()
    {
        return animator.GetCurrentAnimatorStateInfo(1).IsTag("hurt");
    }

    public override IEnumerator KnockBack(Vector2 attackDirection, float force)
    {

        // no knockback if player is blocking
        if (isBlocking)
        {
            rigidBody.velocity = Vector2.zero;
            yield break;
        }

        movementController.StopMoving();

        Debug.Log(attackDirection);
        rigidBody.velocity = attackDirection * force;

        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector2.zero;
    }

    private void Awake()
    {
        // initiate player reference to current instance
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // temporary add all artifacts
        artifacts.Add(playerShield);
        artifacts.Add(playerDashBoots);
        artifacts.Add(playerMagicStaff);
        artifacts.Add(playerEtherealPendant);
    }

    protected override void Update()
    {
        base.Update();

        if (health <= 0 && !GameSceneManager.Instance.deathSceneActive)
        {
            StartCoroutine(GameSceneManager.Instance.PlayerDied(this));
        }

        if (PlayerTakingDamage())
        {
            gameObject.tag = "PlayerHurt";
        }
        else
        {
            gameObject.tag = "Player";
        }
    }
}
