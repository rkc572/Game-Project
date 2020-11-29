using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEtherealPendant : PlayerItem
{
    public Player player;
    public SpriteRenderer spriteRenderer;
    public Collider2D playerCollider;
    public Collider2D etherealCollider;

    public bool ethereal = false;

    float etherealMaxTime = 15.0f;
    float etherealStartTime = 0.0f;

    IEnumerator ManaConsumption()
    {
        if (Player.Instance.mana >= 5.0f)
            Player.Instance.playerSounds.PlayPendantActivationSFX();
        else
        {
            yield break;
        }
        while (ethereal)
        {
            if (Player.Instance.mana < 5.0f)
            {
                player.playerSounds.PlayInvalidInputSFX();
                ethereal = false;
                Player.Instance.playerSounds.PlayPendantDeactivationSFX();
                yield break;
            }

            Player.Instance.ModifyManaByAmount(-5.0f);

            yield return new WaitForSeconds(1);
        }
    }


    IEnumerator EarthEthereal()
    {
        Debug.Log("Turning ethereal");
        etherealStartTime = Time.time;
        ethereal = true;
        playerCollider.gameObject.layer = 11; // ETHEREAL LAYER

        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        while (ethereal)
        {
            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            etherealCollider.OverlapCollider(enemyFilter, enemyColliders);

            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemies hit by collider now take 50% more damage
                    enemy.ToggleEffectState(new VulnerableEffect(enemy, 10.0f, 1.5f));
                }
            }

            yield return null;
        }
        
        
        // player fortified
        player.ToggleEffectState(new FortifiedEffect(player, 10.0f, 0.5f));
        
        //Reset Ethereal
        Debug.Log("UnTurning ethereal");
        ethereal = false;
        playerCollider.gameObject.layer = 10; // PLAYER LAYER
    }

    IEnumerator FireEthereal()
    {
        Debug.Log("Turning fire ethereal");
        etherealStartTime = Time.time;
        ethereal = true;
        playerCollider.gameObject.layer = 11; // ETHEREAL LAYER

        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));


        while (ethereal)
        {

            Debug.Log("fire ethereal checking");

            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            etherealCollider.OverlapCollider(enemyFilter, enemyColliders);

            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemies hit by collider set on fire
                    Debug.Log("enemy set on fire");
                    enemy.ToggleEffectState(new BurningEffect(enemy, 1.0f, 5.0f, 5.0f * player.elementalAttackMultiplier));
                }
            }

            yield return null;
        }

        //Reset Ethereal
        Debug.Log("UnTurning ethereal");
        ethereal = false;
        playerCollider.gameObject.layer = 10; // PLAYER LAYER
    }

    IEnumerator WaterEthereal()
    {
        Debug.Log("Turning ethereal");
        etherealStartTime = Time.time;
        ethereal = true;
        playerCollider.gameObject.layer = 11; // ETHEREAL LAYER

        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        float lastLeechTime = 0.0f;
        float leechInterval = 1.0f;


        while (ethereal)
        {
            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            etherealCollider.OverlapCollider(enemyFilter, enemyColliders);

            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                if (enemy != null && Time.time > lastLeechTime + leechInterval)
                {
                    // enemies hit by collider health leeched
                    enemy.InflictElementalDamage(Mathf.Clamp(2.0f * player.elementalAttackMultiplier, 0.0f, enemy.health));
                    player.ModifyHealthByAmount(Mathf.Clamp(2.0f * player.elementalAttackMultiplier, 0.0f, enemy.health));
                    lastLeechTime = Time.time;
                }
            }

            yield return null;
        }

        //Reset Ethereal
        Debug.Log("UnTurning ethereal");
        ethereal = false;
        playerCollider.gameObject.layer = 10; // PLAYER LAYER
    }


    IEnumerator AirEthereal()
    {
        Debug.Log("Turning ethereal");
        etherealStartTime = Time.time;
        ethereal = true;
        playerCollider.gameObject.layer = 11; // ETHEREAL LAYER

        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        while (ethereal)
        {
            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            etherealCollider.OverlapCollider(enemyFilter, enemyColliders);

            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemies hit by collider slowed down
                    enemy.ToggleEffectState(new SlowedEffect(enemy, 10.0f, 0.25f));
                }
            }

            // player speed boost
            player.ToggleEffectState(new AgileEffect(player, 10.0f, 1.5f));

            yield return null;
        }

        //Reset Ethereal
        Debug.Log("UnTurning ethereal");
        ethereal = false;
        playerCollider.gameObject.layer = 10; // PLAYER LAYER

    }

    IEnumerator Ethereal()
    {
        Debug.Log("Turning ethereal");
        etherealStartTime = Time.time;
        ethereal = true;
        playerCollider.gameObject.layer = 11; // ETHEREAL LAYER


        while (ethereal)
        {
            yield return null;
        }

        //Reset Ethereal
        Debug.Log("UnTurning ethereal");
        ethereal = false;
        playerCollider.gameObject.layer = 10; // PLAYER LAYER

    }

    void ElementalEthereal()
    {
        Debug.Log("elemental ethereal");

        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                StartCoroutine(Ethereal());
                break;
            case ElementalAttribute.EARTH:
                StartCoroutine(EarthEthereal());
                break;
            case ElementalAttribute.FIRE:
                StartCoroutine(FireEthereal());
                break;
            case ElementalAttribute.WATER:
                StartCoroutine(WaterEthereal());
                break;
            case ElementalAttribute.AIR:
                StartCoroutine(AirEthereal());
                break;
        }
    }

    public override void DetectInput()
    {
        if (!ethereal && Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ElementalEthereal();
                StartCoroutine(ManaConsumption());
            }
            else
            {
                StartCoroutine(Ethereal());
                StartCoroutine(ManaConsumption());
            }
        }
        else if (ethereal && Input.GetMouseButtonDown(1))
        {
            //Reset Ethereal
            Debug.Log("UnTurning ethereal");
            ethereal = false;
            playerCollider.gameObject.layer = 10; // PLAYER LAYER

            Player.Instance.playerSounds.PlayPendantDeactivationSFX();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // cancel ethereal if time runs out
        if (ethereal && Time.time > etherealStartTime + etherealMaxTime)
        {
            ethereal = false;

            Player.Instance.playerSounds.PlayPendantDeactivationSFX();
        }

        player.isEthereal = ethereal;

        spriteRenderer.material.color = ethereal ? new Color(1.0f, 1.0f, 1.0f, 0.5f) : new Color(1.0f, 1.0f, 1.0f, 1.0f);

    }
}
