using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTree : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteList;

    void ChooseSprite()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(0, 18);
        spriteRenderer.sprite = spriteList[num];
    }

void Start()
    {
        ChooseSprite();
    }
}
