using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicStaff : PlayerItem
{

    Vector2 attackDirection;


    void RegularMagic()
    {
        //return not enough mana
        if (Player.Instance.mana < 50.0f)
        {
            Player.Instance.playerSounds.PlayInvalidInputSFX();
            return;
        }

        Player.Instance.animator.SetTrigger("Magic");

        Player.Instance.playerSounds.PlayMagicStaffSFX();
        Player.Instance.ModifyManaByAmount(-50.0f);

        var projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerMagicProjectile", typeof(PlayerMagicProjectile));

        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var projectile = GameObject.Instantiate(projectilePrefab, Player.Instance.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        projectile.transform.eulerAngles = new Vector3(projectile.transform.position.x, projectile.transform.position.y, angle);
        projectile.elementalAttribute = ElementalAttribute.NONE;
        projectile.rigidBody.velocity = attackDirection * 1.5f;
    }

    public void Magic()
    {
        LineOfSight lineOfSight = Player.Instance.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                attackDirection = new Vector2(0.0f, 1.0f);
                break;
            case LineOfSight.LEFT:
                attackDirection = new Vector2(-1.0f, 0.0f);
                break;
            case LineOfSight.DOWN:
                attackDirection = new Vector2(0.0f, -1.0f);
                break;
            case LineOfSight.RIGHT:
                attackDirection = new Vector2(1.0f, 0.0f);
                break;
        }

        RegularMagic();
    }


    public void ElementalMagic()
    {

        LineOfSight lineOfSight = Player.Instance.GetLineOfSight();

        switch (lineOfSight)
        {
            case LineOfSight.UP:
                attackDirection = new Vector2(0.0f, 1.0f);
                break;
            case LineOfSight.LEFT:
                attackDirection = new Vector2(-1.0f, 0.0f);
                break;
            case LineOfSight.DOWN:
                attackDirection = new Vector2(0.0f, -1.0f);
                break;
            case LineOfSight.RIGHT:
                attackDirection = new Vector2(1.0f, 0.0f);
                break;
        }

        //return not enough mana
        if (Player.Instance.mana < 100.0f || elementalAttribute == ElementalAttribute.NONE)
        {
            Player.Instance.playerSounds.PlayInvalidInputSFX();
            return;
        }

        Player.Instance.animator.SetTrigger("Magic");
        Player.Instance.ModifyManaByAmount(-100.0f);
        Player.Instance.playerSounds.PlayMagicStaffSFX();

        var projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerMagicProjectile", typeof(PlayerMagicProjectile));

        switch (elementalAttribute)
        {
            case ElementalAttribute.NONE:
                return;
            case ElementalAttribute.EARTH:
                projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerEarthMagicProjectile", typeof(PlayerMagicProjectile));
                playerSounds.PlayEarthSFX();
                break;
            case ElementalAttribute.FIRE:
                projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerFireMagicProjectile", typeof(PlayerMagicProjectile));
                playerSounds.PlayFireSFX();
                break;
            case ElementalAttribute.WATER:
                projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerWaterMagicProjectile", typeof(PlayerMagicProjectile));
                playerSounds.PlayWaterSFX();
                break;
            case ElementalAttribute.AIR:
                projectilePrefab = (PlayerMagicProjectile)Resources.Load("prefabs/PlayerAirMagicProjectile", typeof(PlayerMagicProjectile));
                playerSounds.PlayWindSFX();
                break;
        }


        float colliderYoffset = 0.09f;
        Vector3 attackOffset = new Vector3(attackDirection.x * 0.13f, attackDirection.y * 0.13f + colliderYoffset, 0.0f);
        var angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg + 90.0f;
        var projectile = GameObject.Instantiate(projectilePrefab, Player.Instance.transform.position + attackOffset / 2.0f + new Vector3(0.0f, 0.03f, 0.0f), Quaternion.identity);
        projectile.transform.eulerAngles = new Vector3(projectile.transform.position.x, projectile.transform.position.y, angle);
        projectile.elementalAttribute = elementalAttribute;
        projectile.rigidBody.velocity = attackDirection * 2.5f;
    }

    public override void DetectInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ElementalMagic();
            }
            else
            {
                Magic();
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
        
    }
}
