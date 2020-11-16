using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Player player;
    public float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (GameSceneManager.Instance.deathSceneActive)
        {
            transform.position = new Vector3(
                player.transform.parent.position.x,
                player.transform.parent.position.y,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.parent.position.x, minX, maxX),
                Mathf.Clamp(player.transform.parent.position.y, minY, maxY),
                transform.position.z);
        }
    }
}
