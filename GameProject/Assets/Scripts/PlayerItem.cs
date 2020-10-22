using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalAttribute{
    Earth,
    Wind,
    Fire,
    Water,
    None
}
public abstract class PlayerItem
{
    protected ElementalAttribute elementalAttribute = ElementalAttribute.None;
    protected Player player;

    public PlayerItem(Player player)
    {
        this.player = player;
    }

    // Regular Action
    public abstract void Action();

    // Elemental Actions
    protected abstract void EarthAction();
    protected abstract void WindAction();
    protected abstract void FireAction();
    protected abstract void WaterAction();
    protected void MissingElementalAction()
    {
        // TODO: Decide what to do, play invalid input sound most likely
        Debug.Log("missing implementation");
    }

    public void ElementalAction()
    {
        switch (elementalAttribute)
        {
            case ElementalAttribute.Earth:
                {
                    EarthAction();
                    break;
                }
            case ElementalAttribute.Wind:
                {
                    WindAction();
                    break;
                }
            case ElementalAttribute.Fire:
                {
                    FireAction();
                    break;
                }
            case ElementalAttribute.Water:
                {
                    WaterAction();
                    break;
                }
            case ElementalAttribute.None:
                {
                    MissingElementalAction();
                    break;
                }
        }
    }
}