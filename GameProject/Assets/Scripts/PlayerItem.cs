using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalAttribute
{
    NONE,
    FIRE,
    EARTH,
    AIR,
    WATER
}

public class PlayerItem : MonoBehaviour
{

    public ElementalAttribute elementalAttribute;
    public PlayerSounds playerSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DetectInput()
    {

    }
}
