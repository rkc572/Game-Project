using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion
{
    public int quantity = 1;
    public Sprite sprite;
    public abstract void Consume();
}
