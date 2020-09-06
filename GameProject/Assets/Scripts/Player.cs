using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    [Header("Player Stats")]
    [Range(0, 1000)]
    public float health = 1000;
    [Range(0, 1000)]
    public float mana = 1000;
    public float speed = 1f;


}
