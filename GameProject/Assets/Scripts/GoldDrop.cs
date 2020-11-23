using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float timeToDestroy = 10.0f;
    float timeSpawned;
    bool flick = false;
    public float value = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeSpawned = Time.time;
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = Player.Instance;
            player.gold += value;
            player.playerSounds.PlayGoldPickupSFX();
            Destroy(gameObject);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        // start blinking once only 25% time left
        if (Time.time - timeSpawned > timeToDestroy * 0.75f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, flick ? 1.0f : 0.5f);
            flick = !flick;
        }
    }
}
