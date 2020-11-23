using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : PlayerItem
{
    public Player player;
    public Collider2D upCollider, leftCollider, downCollider, rightCollider;
    public Collider2D earthUpCollider, earthLeftCollider, earthDownCollider, earthRightCollider;
    Collider2D activeShieldCollider, earthShieldCollider;

    Vector3 shieldDeploymentPosition;

    bool shieldDeployed = false;

    float timeElementalShieldDeployed = 0.0f;

    Vector2 attackDirection;

    public override void DetectInput()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                ElementalShield();
            }
            else
            {
                Shield();
            }
        }
        else if (shieldDeployed)
        {
            shieldDeployed = false;
            activeShieldCollider.gameObject.SetActive(false);
            earthShieldCollider.gameObject.SetActive(false);
            player.animator.SetBool("Shield", false);
            player.inputController.detectMovementInput = true;
            player.inputController.detectActionInput = true;
        }
    }

    void RegularShield()
    {

        // do not continue if shield is already deployed
        if (shieldDeployed)
        {
            return;
        }

        shieldDeployed = true;
        shieldDeploymentPosition = player.transform.position;
        activeShieldCollider.gameObject.SetActive(true);
        player.inputController.detectMovementInput = false;
        player.inputController.detectActionInput = false;
        player.movementController.StopMoving();
        player.animator.SetBool("Shield", true);

        // get active shield collider
        Collider2D shieldCollider = activeShieldCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in shield collider
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with shield collider
        shieldCollider.OverlapCollider(enemyFilter, enemyColliders);

        // iterate over all enemies blocked by shield
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // knockback enemies
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));
            }
        }
    }

    void EarthShield()
    {
        if (shieldDeployed)
        {

            if (player.mana == 0.0f)
            {
                // stop shielding if mana runs out
                shieldDeployed = false;
                activeShieldCollider.gameObject.SetActive(false);
                earthShieldCollider.gameObject.SetActive(false);
                player.animator.SetBool("Shield", false);
                player.inputController.detectMovementInput = true;
                player.inputController.detectActionInput = true;
            }

            if (Time.time >= timeElementalShieldDeployed + 1.0f)
            {
                timeElementalShieldDeployed = Time.time;
                // consume 1 mana every second held
                player.ModifyManaByAmount(-1.0f);
            }

            // do not continue if shield is already deployed
            return;
        }

        if (player.mana < 10)
        {
            // end function player does not have enough mana
            return;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        shieldDeployed = true;
        shieldDeploymentPosition = player.transform.position;
        activeShieldCollider.gameObject.SetActive(true);
        earthShieldCollider.gameObject.SetActive(true);
        player.inputController.detectMovementInput = false;
        player.inputController.detectActionInput = false;
        player.movementController.StopMoving();
        player.animator.SetBool("Shield", true);
        playerSounds.PlayEarthSFX();

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active shield collider
        Collider2D shieldCollider = activeShieldCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in shield collider
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with shield collider
        shieldCollider.OverlapCollider(enemyFilter, enemyColliders);

        // iterate over all enemies blocked by shield
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // knockback enemies
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));
            }
        }

    }

    void FireShield()
    {

    }

    void WaterShield()
    {

    }

    void AirShield()
    {

    }

    void ElementalShield()
    {
        LineOfSight lineOfSight = player.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                activeShieldCollider = upCollider;
                earthShieldCollider = earthUpCollider;
                attackDirection = new Vector2(0.0f, 1.0f);
                break;
            case LineOfSight.LEFT:
                activeShieldCollider = leftCollider;
                earthShieldCollider = earthLeftCollider;
                attackDirection = new Vector2(-1.0f, 0.0f);
                break;
            case LineOfSight.DOWN:
                activeShieldCollider = downCollider;
                earthShieldCollider = earthDownCollider;
                attackDirection = new Vector2(0.0f, -1.0f);
                break;
            case LineOfSight.RIGHT:
                activeShieldCollider = rightCollider;
                earthShieldCollider = earthRightCollider;
                attackDirection = new Vector2(1.0f, 0.0f);
                break;
        }

        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                RegularShield();
                break;
            case ElementalAttribute.EARTH:
                EarthShield();
                break;
            case ElementalAttribute.FIRE:
                FireShield();
                break;
            case ElementalAttribute.WATER:
                WaterShield();
                break;
            case ElementalAttribute.AIR:
                AirShield();
                break;
        }
    }

    void Shield()
    {
        LineOfSight lineOfSight = player.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                activeShieldCollider = upCollider;
                earthShieldCollider = earthUpCollider;
                attackDirection = new Vector2(0.0f, 1.0f);
                break;
            case LineOfSight.LEFT:
                activeShieldCollider = leftCollider;
                earthShieldCollider = earthLeftCollider;
                attackDirection = new Vector2(-1.0f, 0.0f);
                break;
            case LineOfSight.DOWN:
                activeShieldCollider = downCollider;
                earthShieldCollider = earthDownCollider;
                attackDirection = new Vector2(0.0f, -1.0f);
                break;
            case LineOfSight.RIGHT:
                activeShieldCollider = rightCollider;
                earthShieldCollider = earthRightCollider;
                attackDirection = new Vector2(1.0f, 0.0f);
                break;
        }

        RegularShield();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldDeployed)
        {
            DetectInput();
            player.isBlocking = true;
            player.transform.position = shieldDeploymentPosition;
        }
    }
}
