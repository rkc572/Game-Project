using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : MonoBehaviour
{

    public Rigidbody2D rigidBody;

    public Mage mage;

    bool returned = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    public void ReturnToSender()
    {
        returned = true;
        Vector3 target = mage.transform.position;
        Vector2 targetDirection = target - transform.position;
        Vector2 newVelocity = targetDirection.normalized * 2.25f;
        rigidBody.velocity = newVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (returned && collision.gameObject.layer == 9) //enemy layer
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.InflictElementalDamage(25.0f);
                enemy.animator.SetTrigger("TookDamage");
                Destroy(gameObject);
            }
        }



        if (collision.gameObject.layer == 8) //WALL LAYER
        {
            Destroy(gameObject);
        }


        var player = collision.GetComponentInParent<Player>();

        if (collision.CompareTag("Player"))
        {
            if (!Player.Instance.isEthereal)
            {
                player.InflictDamage(25.0f);
                player.animator.SetTrigger("PlayerHurt");
            }
            Destroy(gameObject);
        }
    }
}
