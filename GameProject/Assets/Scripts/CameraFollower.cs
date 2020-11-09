﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Player player;
    public float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(player.transform.parent.position.x, minX, maxX),
            Mathf.Clamp(player.transform.parent.position.y, minY, maxY),
            transform.position.z);
    }
}
