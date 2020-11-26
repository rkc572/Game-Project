using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashBoots : PlayerItem
{

    public Player player;
    bool dashing = false;
    float dashTime = 0.25f;
    public ParticleSystem dashParticles;
    public ParticleSystem afterImage;
    public SpriteRenderer spriteRenderer;
    public Collider2D playerDashHitBox;
    public Collider2D freezeCollider;

    public List<Sprite> moveUpSprites;
    public List<Sprite> moveLeftSprites;
    public List<Sprite> moveRightSprites;
    public List<Sprite> moveDownSprites;

    List<Sprite> movingSprites;

    void SetAfterImageSprites()
    {
        for (int i = 0; i < afterImage.textureSheetAnimation.spriteCount; i++)
        {
            afterImage.textureSheetAnimation.SetSprite(i, movingSprites[i]);
        }
    }

    IEnumerator RegularDash()
    {

        if (player.mana < 10)
        {
            // end function player does not have enough mana
            StopDashing();
            yield break;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);


        // Move Player in direction player is facing
        LineOfSight lineOfSight = player.GetLineOfSight();

        Vector2 dashDirection = Vector2.zero;

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                dashDirection = new Vector2(0.0f, 1.0f);
                movingSprites = moveUpSprites;
                break;
            case LineOfSight.LEFT:
                dashDirection = new Vector2(-1.0f, 0.0f);
                movingSprites = moveLeftSprites;
                break;
            case LineOfSight.DOWN:
                dashDirection = new Vector2(0.0f, -1.0f);
                movingSprites = moveDownSprites;
                break;
            case LineOfSight.RIGHT:
                dashDirection = new Vector2(1.0f, 0.0f);
                movingSprites = moveRightSprites;
                break;
        }

        SetAfterImageSprites();

        float dashStartTime = Time.time;

        Debug.Log("Dashing");
        player.animator.SetBool("Moving", true);
        dashParticles.gameObject.SetActive(true);
        afterImage.gameObject.SetActive(true);
        bool startedDashingWhileTouchingWall = playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall"));
        player.playerSounds.playerMovementAudioSource.pitch = 3.0f;

        while (Time.time < dashStartTime + dashTime)
        {
            player.inputController.detectMovementInput = false;
            player.inputController.detectActionInput = false;

            bool moveUp = Input.GetKey(KeyCode.W);
            bool moveLeft = Input.GetKey(KeyCode.A);
            bool moveDown = Input.GetKey(KeyCode.S);
            bool moveRight = Input.GetKey(KeyCode.D);

            float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
            float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

            player.rigidBody.velocity = dashDirection * 4.0f + player.speed * new Vector2(horizontalVelocity, verticalVelocity).normalized;

            // Contact filter to only include colliders in Enemy layer
            ContactFilter2D enemyFilter = new ContactFilter2D();
            enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            // stop dashing if player hits wall and wasn't touching wall when first dashing
            if (!startedDashingWhileTouchingWall && playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                StopDashing();

                // take damage for dashing into wall
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            // get all enemy colliders overlapping with sword swing collider
            playerDashHitBox.OverlapCollider(enemyFilter, enemyColliders);

            // Check if player hit enemy collider while dashing and do not do enemy collider calculations if player is ethereal
            if (enemyColliders.Count > 0 && !player.isEthereal)
            {

                // knockback all enemies collided with
                foreach (Collider2D enemyCollider in enemyColliders)
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Dashed into enemy");
                        enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.15f, dashDirection, 2.0f));
                    }
                }

                StopDashing();

                // take damage for dashing into enemy
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            yield return null;
        }

        StopDashing();
    }


    IEnumerator EarthDash()
    {

        if (player.mana < 100)
        {
            // end function player does not have enough mana
            StopDashing();
            yield break;
        }

        // consume mana for action
        player.ModifyManaByAmount(-100.0f);


        // Move Player in direction player is facing
        LineOfSight lineOfSight = player.GetLineOfSight();

        Vector2 dashDirection = Vector2.zero;

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                dashDirection = new Vector2(0.0f, 1.0f);
                movingSprites = moveUpSprites;
                break;
            case LineOfSight.LEFT:
                dashDirection = new Vector2(-1.0f, 0.0f);
                movingSprites = moveLeftSprites;
                break;
            case LineOfSight.DOWN:
                dashDirection = new Vector2(0.0f, -1.0f);
                movingSprites = moveDownSprites;
                break;
            case LineOfSight.RIGHT:
                dashDirection = new Vector2(1.0f, 0.0f);
                movingSprites = moveRightSprites;
                break;
        }

        SetAfterImageSprites();

        float dashStartTime = Time.time;

        Debug.Log("Dashing");
        player.animator.SetBool("Moving", true);
        dashParticles.gameObject.SetActive(true);
        afterImage.gameObject.SetActive(true);
        bool startedDashingWhileTouchingWall = playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall"));
        player.playerSounds.playerMovementAudioSource.pitch = 3.0f;
        player.playerSounds.PlayEarthSFX();

        while (Time.time < dashStartTime + dashTime)
        {
            player.inputController.detectMovementInput = false;
            player.inputController.detectActionInput = false;

            bool moveUp = Input.GetKey(KeyCode.W);
            bool moveLeft = Input.GetKey(KeyCode.A);
            bool moveDown = Input.GetKey(KeyCode.S);
            bool moveRight = Input.GetKey(KeyCode.D);

            float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
            float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

            player.rigidBody.velocity = dashDirection * 4.0f + player.speed * new Vector2(horizontalVelocity, verticalVelocity).normalized;

            // Contact filter to only include colliders in Enemy layer
            ContactFilter2D enemyFilter = new ContactFilter2D();
            enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            // stop dashing if player hits wall and wasn't touching wall when first dashing
            if (!startedDashingWhileTouchingWall && playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                StopDashing();
                // apply fortified effect
                player.ToggleEffectState(new FortifiedEffect(player, 10.0f, 0.5f));
                yield break;
            }

            // get all enemy colliders overlapping with sword swing collider
            playerDashHitBox.OverlapCollider(enemyFilter, enemyColliders);

            // Check if player hit enemy collider while dashing and do not do enemy collider calculations if player is ethereal
            if (enemyColliders.Count > 0 && !player.isEthereal)
            {

                // knockback all enemies collided with
                foreach (Collider2D enemyCollider in enemyColliders)
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Dashed into enemy");
                        enemy.InflictElementalDamage(15.0f * player.elementalAttackMultiplier);
                        enemy.animator.SetTrigger("TookDamage");
                        enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.15f, dashDirection, 2.0f));
                    }
                }

                StopDashing();
                // apply fortified effect
                player.ToggleEffectState(new FortifiedEffect(player, 10.0f, 0.5f));
                yield break;
            }

            yield return null;
        }

        StopDashing();
        // apply fortified effect
        player.ToggleEffectState(new FortifiedEffect(player, 10.0f, 0.5f));
    }


    IEnumerator FireDash()
    {

        if (player.mana < 100)
        {
            // end function player does not have enough mana
            StopDashing();
            yield break;
        }

        // consume mana for action
        player.ModifyManaByAmount(-10.0f);

        // Move Player in direction player is facing
        LineOfSight lineOfSight = player.GetLineOfSight();

        Vector2 dashDirection = Vector2.zero;

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                dashDirection = new Vector2(0.0f, 1.0f);
                movingSprites = moveUpSprites;
                break;
            case LineOfSight.LEFT:
                dashDirection = new Vector2(-1.0f, 0.0f);
                movingSprites = moveLeftSprites;
                break;
            case LineOfSight.DOWN:
                dashDirection = new Vector2(0.0f, -1.0f);
                movingSprites = moveDownSprites;
                break;
            case LineOfSight.RIGHT:
                dashDirection = new Vector2(1.0f, 0.0f);
                movingSprites = moveRightSprites;
                break;
        }

        SetAfterImageSprites();

        float dashStartTime = Time.time;

        Debug.Log("Dashing");
        player.animator.SetBool("Moving", true);
        dashParticles.gameObject.SetActive(true);
        afterImage.gameObject.SetActive(true);
        bool startedDashingWhileTouchingWall = playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall"));
        player.playerSounds.playerMovementAudioSource.pitch = 3.0f;
        player.playerSounds.PlayFireSFX();

        while (Time.time < dashStartTime + dashTime)
        {

            // GENERATE FIRE TRAIL
            var fireTrailPrefab = (GameObject)Resources.Load("prefabs/FireTrail", typeof(GameObject));
            GameObject.Instantiate(fireTrailPrefab, player.transform.position, Quaternion.identity);

            player.inputController.detectMovementInput = false;
            player.inputController.detectActionInput = false;

            bool moveUp = Input.GetKey(KeyCode.W);
            bool moveLeft = Input.GetKey(KeyCode.A);
            bool moveDown = Input.GetKey(KeyCode.S);
            bool moveRight = Input.GetKey(KeyCode.D);

            float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
            float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

            player.rigidBody.velocity = dashDirection * 4.0f + player.speed * new Vector2(horizontalVelocity, verticalVelocity).normalized;

            // Contact filter to only include colliders in Enemy layer
            ContactFilter2D enemyFilter = new ContactFilter2D();
            enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            // stop dashing if player hits wall and wasn't touching wall when first dashing
            if (!startedDashingWhileTouchingWall && playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                StopDashing();

                // take damage for dashing into wall
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            // get all enemy colliders overlapping with sword swing collider
            playerDashHitBox.OverlapCollider(enemyFilter, enemyColliders);

            // Check if player hit enemy collider while dashing and do not do enemy collider calculations if player is ethereal
            if (enemyColliders.Count > 0 && !player.isEthereal)
            {

                // knockback all enemies collided with
                foreach (Collider2D enemyCollider in enemyColliders)
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Dashed into enemy");
                        enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.15f, dashDirection, 2.0f));
                    }
                }

                StopDashing();

                // take damage for dashing into enemy
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            yield return null;
        }

        StopDashing();
    }


    IEnumerator WaterDash()
    {

        if (player.mana < 100)
        {
            // end function player does not have enough mana
            StopDashing();
            yield break;
        }

        // consume mana for action
        player.ModifyManaByAmount(-100.0f);


        // Move Player in direction player is facing
        LineOfSight lineOfSight = player.GetLineOfSight();

        Vector2 dashDirection = Vector2.zero;

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                dashDirection = new Vector2(0.0f, 1.0f);
                movingSprites = moveUpSprites;
                break;
            case LineOfSight.LEFT:
                dashDirection = new Vector2(-1.0f, 0.0f);
                movingSprites = moveLeftSprites;
                break;
            case LineOfSight.DOWN:
                dashDirection = new Vector2(0.0f, -1.0f);
                movingSprites = moveDownSprites;
                break;
            case LineOfSight.RIGHT:
                dashDirection = new Vector2(1.0f, 0.0f);
                movingSprites = moveRightSprites;
                break;
        }

        SetAfterImageSprites();

        float dashStartTime = Time.time;

        Debug.Log("Dashing");
        player.animator.SetBool("Moving", true);
        dashParticles.gameObject.SetActive(true);
        afterImage.gameObject.SetActive(true);
        bool startedDashingWhileTouchingWall = playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall"));
        player.playerSounds.playerMovementAudioSource.pitch = 3.0f;
        player.playerSounds.PlayWaterSFX();

        // Contact filter to only include colliders in Enemy layer
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        // list to store all enemy colliders in swing
        var enemyColliders = new List<Collider2D>();


        /*
        //freeze enemies within freezeCollider
        freezeCollider.OverlapCollider(enemyFilter, enemyColliders);

        foreach (Collider2D enemyCollider in enemyColliders)
        {
            Enemy enemy = enemyCollider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Froze enemy");
                enemy.ToggleEffectState(new FrozenEffect(enemy, 8.0f, 1.5f));
            }
        }
        */

        while (Time.time < dashStartTime + dashTime)
        {
            var waterTrailPrefab = (GameObject)Resources.Load("prefabs/WaterTrail", typeof(GameObject));
            GameObject.Instantiate(waterTrailPrefab, player.transform.position, Quaternion.identity);

            player.inputController.detectMovementInput = false;
            player.inputController.detectActionInput = false;

            bool moveUp = Input.GetKey(KeyCode.W);
            bool moveLeft = Input.GetKey(KeyCode.A);
            bool moveDown = Input.GetKey(KeyCode.S);
            bool moveRight = Input.GetKey(KeyCode.D);

            float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
            float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

            player.rigidBody.velocity = dashDirection * 4.0f + player.speed * new Vector2(horizontalVelocity, verticalVelocity).normalized;

            enemyColliders = new List<Collider2D>();

            // stop dashing if player hits wall and wasn't touching wall when first dashing
            if (!startedDashingWhileTouchingWall && playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                StopDashing();

                // take damage for dashing into wall
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            // get all enemy colliders overlapping with sword swing collider
            playerDashHitBox.OverlapCollider(enemyFilter, enemyColliders);

            // Check if player hit enemy collider while dashing and do not do enemy collider calculations if player is ethereal
            if (enemyColliders.Count > 0 && !player.isEthereal)
            {

                // knockback all enemies collided with
                foreach (Collider2D enemyCollider in enemyColliders)
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Dashed into enemy");
                        enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.15f, dashDirection, 2.0f));
                    }
                }

                StopDashing();

                // take damage for dashing into enemy
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            yield return null;
        }

        StopDashing();
    }


    IEnumerator AirDash()
    {

        if (player.mana < 10)
        {
            // end function player does not have enough mana
            StopDashing();
            yield break;
        }

        // consume mana for action
        player.ModifyManaByAmount(-100.0f);

        // Move Player in direction player is facing
        LineOfSight lineOfSight = player.GetLineOfSight();

        Vector2 dashDirection = Vector2.zero;

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                dashDirection = new Vector2(0.0f, 1.0f);
                movingSprites = moveUpSprites;
                break;
            case LineOfSight.LEFT:
                dashDirection = new Vector2(-1.0f, 0.0f);
                movingSprites = moveLeftSprites;
                break;
            case LineOfSight.DOWN:
                dashDirection = new Vector2(0.0f, -1.0f);
                movingSprites = moveDownSprites;
                break;
            case LineOfSight.RIGHT:
                dashDirection = new Vector2(1.0f, 0.0f);
                movingSprites = moveRightSprites;
                break;
        }

        SetAfterImageSprites();

        float dashStartTime = Time.time;

        Debug.Log("Dashing");
        player.animator.SetBool("Moving", true);
        dashParticles.gameObject.SetActive(true);
        afterImage.gameObject.SetActive(true);
        bool startedDashingWhileTouchingWall = playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall"));
        player.playerSounds.playerMovementAudioSource.pitch = 3.0f;
        player.playerSounds.PlayWindSFX();

        while (Time.time < dashStartTime + (dashTime / 2.0f))
        {
            player.inputController.detectMovementInput = false;
            player.inputController.detectActionInput = false;

            bool moveUp = Input.GetKey(KeyCode.W);
            bool moveLeft = Input.GetKey(KeyCode.A);
            bool moveDown = Input.GetKey(KeyCode.S);
            bool moveRight = Input.GetKey(KeyCode.D);

            float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
            float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

            player.rigidBody.velocity = dashDirection * 4.0f * 2.0f + player.speed * new Vector2(horizontalVelocity, verticalVelocity).normalized;

            // Contact filter to only include colliders in Enemy layer
            ContactFilter2D enemyFilter = new ContactFilter2D();
            enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));

            // list to store all enemy colliders in swing
            var enemyColliders = new List<Collider2D>();

            // stop dashing if player hits wall and wasn't touching wall when first dashing
            if (!startedDashingWhileTouchingWall && playerDashHitBox.IsTouchingLayers(LayerMask.GetMask("Wall")))
            {
                StopDashing();
                // apply speed boost
                player.ToggleEffectState(new AgileEffect(player, 10.0f, 2.0f));

                // take damage for dashing into wall
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            // get all enemy colliders overlapping with sword swing collider
            playerDashHitBox.OverlapCollider(enemyFilter, enemyColliders);

            // Check if player hit enemy collider while dashing and do not do enemy collider calculations if player is ethereal
            if (enemyColliders.Count > 0 && !player.isEthereal)
            {

                // knockback all enemies collided with
                foreach (Collider2D enemyCollider in enemyColliders)
                {
                    Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Debug.Log("Dashed into enemy");
                        enemy.ToggleEffectState(new RepulsedEffect(enemy, 0.15f, dashDirection, 2.0f));
                    }
                }

                StopDashing();
                // apply speed boost
                player.ToggleEffectState(new AgileEffect(player, 10.0f, 2.0f));

                // take damage for dashing into enemy
                player.InflictPhysicalDamage(5.0f);
                player.animator.SetTrigger("PlayerHurt");
                StartCoroutine(player.KnockBack(-dashDirection, 2.0f));
                yield break;
            }

            yield return null;
        }

        StopDashing();
        // apply speed boost
        player.ToggleEffectState(new AgileEffect(player, 10.0f, 2.0f));
    }


    void StopDashing()
    {
        // stop dashing
        player.animator.SetBool("Moving", false);
        dashParticles.gameObject.SetActive(false);
        afterImage.gameObject.SetActive(false);
        dashing = false;
        player.inputController.detectMovementInput = true;
        player.inputController.detectActionInput = true;
        player.rigidBody.velocity = Vector2.zero;
        player.playerSounds.playerMovementAudioSource.pitch = 1.0f;
    }

    void ElementalDash()
    {
        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                StartCoroutine(RegularDash());
                break;
            case ElementalAttribute.EARTH:
                StartCoroutine(EarthDash());
                break;
            case ElementalAttribute.FIRE:
                StartCoroutine(FireDash());
                break;
            case ElementalAttribute.WATER:
                StartCoroutine(WaterDash());
                break;
            case ElementalAttribute.AIR:
                StartCoroutine(AirDash());
                break;
        }
    }

    void Dash()
    {
        dashing = true;
        player.inputController.detectMovementInput = false;
        player.inputController.detectActionInput = false;
        StartCoroutine(RegularDash());
    }


    public override void DetectInput()
    {
        if (!dashing && Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ElementalDash();
            }
            else
            {
                Dash();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dashing && player.animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            player.animator.SetFloat("MoveAnimationSpeedMultiplier", player.rigidBody.velocity.magnitude);
        }
    }
}
