using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Mob
{
    // public Player instance reference variable
    public static Player Instance;

    // PlayerItem variables
    public PlayerItem playerSword, playerShield, playerDashBoots, playerMagicStaff, playerEtherealPendant, selectedArtifact;
    public List<PlayerItem> artifacts = new List<PlayerItem>();

    // Acquired upgrades
    [Tooltip("0:fire, 1:water, 2:air, 3:earth")]
    public bool[] hasUpgrades = new bool[4];

    // Acquired items
    [Tooltip("0:staff, 1:pendant, 2:boots, 3:shield")]
    public bool[] hasItems = new bool[4];

    // Clear status of room 1 of each sub dungeon (to fix returning from room 2 to 1)
    [Tooltip("0:staff, 1:pendant, 2:boots, 3:shield")]
    public bool[] clearedRooms = new bool[4];

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


    public void AddPotion(Potion potion)
    {
        // Add Potion only if not already in potion list
        if (!potions.Any(inventoryPotion => inventoryPotion.GetType() == potion.GetType()))
        {
            potions.Add(potion);
        }
        // Else increment quantity
        else
        {
            for (int i = 0; i < potions.Count; i++)
            {
                if (potions[i].GetType() == potion.GetType())
                {
                    potions[i].quantity++;
                    break;
                }
            }
        }
        selectedPotion = potions[potions.Count / 2];
        print("POTIONS: ");
        foreach (Potion invPot in potions)
        {
            var name = invPot.GetType().Name;
            var quant = invPot.quantity;
            print($"{name} {quant}");
        }
        print($"Selected potion: {selectedPotion.GetType().Name} {selectedPotion.quantity}");
    }


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
        //artifacts.Add(playerShield);
        //artifacts.Add(playerDashBoots);
        //artifacts.Add(playerMagicStaff);
        //artifacts.Add(playerEtherealPendant);
    }

    protected override void Update()
    {
        base.Update();




        // TESTING/DEMO PURPOSES ONLY

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerSword.elementalAttribute = ElementalAttribute.AIR;
            selectedArtifact.elementalAttribute = ElementalAttribute.AIR;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerSword.elementalAttribute = ElementalAttribute.EARTH;
            selectedArtifact.elementalAttribute = ElementalAttribute.EARTH;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerSword.elementalAttribute = ElementalAttribute.FIRE;
            selectedArtifact.elementalAttribute = ElementalAttribute.FIRE;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playerSword.elementalAttribute = ElementalAttribute.WATER;
            selectedArtifact.elementalAttribute = ElementalAttribute.WATER;
        }

        // TESTING/DEMO PURPOSES ONLY












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
