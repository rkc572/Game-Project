using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // do not do collider calculations if player is ethereal
        if (Player.Instance.isEthereal)
            return;

        if (collider.gameObject.layer == 9) // ENEMY LAYER
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("attack blocked is true");
                enemy.attackBlocked = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // do not do collider calculations if player is ethereal
        if (Player.Instance.isEthereal)
            return;

        if (collider.gameObject.layer == 9) // ENEMY LAYER
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("attack blocked is false");
                enemy.attackBlocked = false;
            }
        }
    }
}
