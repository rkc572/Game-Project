using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : PlayerItem
{
    public Player player;
    public Collider2D upCollider, leftCollider, downCollider, rightCollider;
    public Collider2D airUpCollider, airLeftCollider, airDownCollider, airRightCollider;

    public Collider2D activeSwordSwingCollider;
    public Collider2D airSwordSwingCollider;

    bool swungSword = false;
    Vector2 attackDirection;

    void RegularSwing()
    {

        // Stop player movement while attacking
        swungSword = true;
        player.inputController.detectMovementInput = false;
        player.movementController.StopMoving();

        // Play sword swing animation
        player.animator.SetTrigger("SwingSword");

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active regular swing collider
        Collider2D swordSwingCollider = activeSwordSwingCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in swing
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with sword swing collider
        swordSwingCollider.OverlapCollider(enemyFilter, enemyColliders);

        // inflict only sword swing damage
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // inflict damage
                enemy.InflictPhysicalDamage(25.0f * player.physicalAttackMultiplier * player.attackMultiplier);
                enemy.animator.SetTrigger("TookDamage");
                // knockback enemy
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));
            }
        }
    }

    void EarthSwing()
    {
        if (player.mana < 10)
        {
            // end function player does not have enough mana
            player.playerSounds.PlayInvalidInputSFX();
            return;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)Resources.Load("prefabs/Earth Swing", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);


        // Stop player movement while attacking
        swungSword = true;
        player.inputController.detectMovementInput = false;
        player.movementController.StopMoving();


        player.animator.SetTrigger("SwingSword");
        playerSounds.PlayEarthSFX();

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active regular swing collider
        Collider2D swordSwingCollider = activeSwordSwingCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in regular swing
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with sword swing collider
        swordSwingCollider.OverlapCollider(enemyFilter, enemyColliders);

        // inflict earth sword swing damage to enemies
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Debug.Log("base damage only");
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // inflict damage
                enemy.InflictElementalDamage(25.0f * player.elementalAttackMultiplier * player.attackMultiplier);
                enemy.animator.SetTrigger("TookDamage");

                // knockback enemy
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));

                // stun enemy
                enemy.ToggleEffectState(new StunnedEffect(enemy, 2.0f));
            }
        }
    }

    void WaterSwing()
    {
        if (player.mana < 10)
        {
            // end function player does not have enough mana
            player.playerSounds.PlayInvalidInputSFX();
            return;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)Resources.Load("prefabs/Water Swing", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);

        // Stop player movement while attacking
        swungSword = true;
        player.inputController.detectMovementInput = false;
        player.movementController.StopMoving();


        player.animator.SetTrigger("SwingSword");
        playerSounds.PlayWaterSFX();

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active regular swing collider
        Collider2D swordSwingCollider = activeSwordSwingCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in regular swing
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with sword swing collider
        swordSwingCollider.OverlapCollider(enemyFilter, enemyColliders);

        // inflict water sword swing damage to enemies
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Debug.Log("base damage only");
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                float damageDealt = 25.0f * player.elementalAttackMultiplier * player.attackMultiplier;

                // inflict damage
                enemy.InflictElementalDamage(damageDealt);
                enemy.animator.SetTrigger("TookDamage");

                // knockback enemy
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));

                // leech health from enemy equal to a small % of damage dealt
                // 20 percent
                player.ModifyHealthByAmount(Mathf.Clamp(damageDealt, 0.0f, enemy.health));
            }
        }
    }

    void FireSwing()
    {
        if (player.mana < 10)
        {
            // end function player does not have enough mana
            player.playerSounds.PlayInvalidInputSFX();
            return;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)Resources.Load("prefabs/Fire Swing", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);

        // Stop player movement while attacking
        swungSword = true;
        player.inputController.detectMovementInput = false;
        player.movementController.StopMoving();


        player.animator.SetTrigger("SwingSword");
        playerSounds.PlayFireSFX();

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active regular swing collider
        Collider2D swordSwingCollider = activeSwordSwingCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in regular swing
        var enemyColliders = new List<Collider2D>();

        // get all enemy colliders overlapping with sword swing collider
        swordSwingCollider.OverlapCollider(enemyFilter, enemyColliders);

        // inflict fire sword swing damage to enemies
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

                Debug.Log("ENEMY SET ON FIRE");


                // inflict damage
                enemy.InflictElementalDamage(25.0f * player.elementalAttackMultiplier * player.attackMultiplier);
                enemy.animator.SetTrigger("TookDamage");

                // knockback enemy
                StartCoroutine(enemy.KnockBack(attackDirection, 2.0f));

                // set enemy on fire
                enemy.ToggleEffectState(new BurningEffect(enemy, 1.0f, 4.0f, 10.0f));
            }
        }
    }

    void AirSwing()
    {
        if (player.mana < 10)
        {
            // end function player does not have enough mana
            player.playerSounds.PlayInvalidInputSFX();
            return;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var swingPrefab = (GameObject)Resources.Load("prefabs/Wind Swing", typeof(GameObject));
        var swingObject = GameObject.Instantiate(swingPrefab, player.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        swingObject.transform.eulerAngles = new Vector3(swingObject.transform.position.x, swingObject.transform.position.y, angle);
        Object.Destroy(swingObject, 0.6f);


        // Stop player movement while attacking
        swungSword = true;
        player.inputController.detectMovementInput = false;
        player.movementController.StopMoving();


        player.animator.SetTrigger("SwingSword");
        playerSounds.PlayWindSFX();

        // do not do collider calculations if player is ethereal
        if (player.isEthereal)
            return;

        // get active regular swing collider
        Collider2D swordSwingCollider = activeSwordSwingCollider;

        // get active air swing collider
        Collider2D airSwingCollider = airSwordSwingCollider;

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

        // list to store all enemy colliders in regular swing
        var enemyColliders = new List<Collider2D>();

        // list to store all enemy colliders hit at the tip of air swing
        var enemyCollidersHitAtTip = new List<Collider2D>();

        // get all enemy colliders overlapping with sword swing collider
        swordSwingCollider.OverlapCollider(enemyFilter, enemyColliders);

        // get all enemy colliders overlapping with air swing collider
        airSwingCollider.OverlapCollider(enemyFilter, enemyCollidersHitAtTip);

        // remove enemy colliders also hit at the tip in enemyColliders
        foreach (Collider2D enemyCollider in enemyCollidersHitAtTip)
        {
            enemyColliders.Remove(enemyCollider);
        }

        // inflict only sword swing damage to enemies not hit at the tip
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // inflict damage
                enemy.InflictElementalDamage(25.0f * player.elementalAttackMultiplier * player.attackMultiplier);
                enemy.animator.SetTrigger("TookDamage");

                // apply stronger repulsed effect
                enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.2f, attackDirection, 4.0f));
            }
        }

        // inflict bonus damage to enemies hit at the tip
        foreach (Collider2D enemyCollider in enemyCollidersHitAtTip)
        {
            Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // inflict damage
                enemy.InflictPhysicalDamage(35.0f * player.physicalAttackMultiplier * player.attackMultiplier);
                enemy.animator.SetTrigger("TookDamage");

                // apply repulsed effect
                enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.2f, attackDirection, 3.0f));
            }
        }

    }


    public override void DetectInput()
    {
        // Mouse button left click to swing sword
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ElementalSwordSwing();
            }
            else
            {
                SwordSwing();
            }
        }
    }
    
    void ElementalSwordSwing()
    {

        LineOfSight lineOfSight = player.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                attackDirection = new Vector2(0.0f, 1.0f);
                activeSwordSwingCollider = upCollider;
                airSwordSwingCollider = airUpCollider;
                break;
            case LineOfSight.LEFT:
                attackDirection = new Vector2(-1.0f, 0.0f);
                activeSwordSwingCollider = leftCollider;
                airSwordSwingCollider = airLeftCollider;
                break;
            case LineOfSight.DOWN:
                attackDirection = new Vector2(0.0f, -1.0f);
                activeSwordSwingCollider = downCollider;
                airSwordSwingCollider = airDownCollider;
                break;
            case LineOfSight.RIGHT:
                attackDirection = new Vector2(1.0f, 0.0f);
                activeSwordSwingCollider = rightCollider;
                airSwordSwingCollider = airRightCollider;
                break;
        }

        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                RegularSwing();
                break;
            case ElementalAttribute.EARTH:
                EarthSwing();
                break;
            case ElementalAttribute.FIRE:
                FireSwing();
                break;
            case ElementalAttribute.WATER:
                WaterSwing();
                break;
            case ElementalAttribute.AIR:
                AirSwing();
                break;
        }
    }

    void SwordSwing()
    {
        LineOfSight lineOfSight = player.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                attackDirection = new Vector2(0.0f, 1.0f);
                activeSwordSwingCollider = upCollider;
                airSwordSwingCollider = airUpCollider;
                break;
            case LineOfSight.LEFT:
                attackDirection = new Vector2(-1.0f, 0.0f);
                activeSwordSwingCollider = leftCollider;
                airSwordSwingCollider = airLeftCollider;
                break;
            case LineOfSight.DOWN:
                attackDirection = new Vector2(0.0f, -1.0f);
                activeSwordSwingCollider = downCollider;
                airSwordSwingCollider = airDownCollider;
                break;
            case LineOfSight.RIGHT:
                attackDirection = new Vector2(1.0f, 0.0f);
                activeSwordSwingCollider = rightCollider;
                airSwordSwingCollider = airRightCollider;
                break;
        }

        RegularSwing();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (swungSword && !player.animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput"))
        {
            player.inputController.detectMovementInput = true;
            swungSword = false;
        }
    }
}
