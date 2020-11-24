using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : MonoBehaviour
{

    public Sprite healthPotionSprite,
        manaPotionSprite,
        regenerationPotionSprite;


    public void SellHealthPotion()
    {
        //TODO money check

        var healthPotion = new HealthPotion();
        healthPotion.sprite = healthPotionSprite;
        Player.Instance.AddPotion(healthPotion);
    }

    public void SellManaPotion()
    {
        //TODO money check

        var manaPotion = new ManaPotion();
        manaPotion.sprite = manaPotionSprite;
        Player.Instance.AddPotion(manaPotion);
    }

    public void SellRegenerationPotion()
    {
        //TODO money check

        var regenerationPotion = new HealthPotion();
        regenerationPotion.sprite = regenerationPotionSprite;
        Player.Instance.AddPotion(regenerationPotion);
    }

}
