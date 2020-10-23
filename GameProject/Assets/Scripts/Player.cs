using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Mob properties;
    public Animator animator;
    public PlayerItem sword;
    public PlayerItem selectedArtifact = null;
    
    public float gold = 0.0f;

    List<PlayerItem> artifacts;
    // List<Potions> potions;


    private void Awake()
    {
        sword = new PlayerSword(this);
    }
}
