using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    
    public Sprite healthPotionSprite,
        manaPotionSprite,
        regenerationPotionSprite;

    public const int HealthPotionPrice = 50;
    public const int ManaPotionPrice = 50;
    public const int RegenPotionPrice = 50;

   
    void Start()
    {
        Debug.Log("Total Gold Count On Entry: " + Player.Instance.gold);
    }
   
    public void SellHealthPotion()
    {
        //TODO money check

        var healthPotion = new HealthPotion(); 
        healthPotion.sprite = healthPotionSprite;

        if(Player.Instance.gold >= HealthPotionPrice)
        {
            Player.Instance.AddPotion(healthPotion);
            Player.Instance.gold -= HealthPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
        
    }

    public void SellManaPotion()
    {
        //TODO money check

        var manaPotion = new ManaPotion();
        manaPotion.sprite = manaPotionSprite;


        if (Player.Instance.gold >= ManaPotionPrice)
        {
            Player.Instance.AddPotion(manaPotion);
            Player.Instance.gold -= ManaPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public void SellRegenerationPotion()
    {
        //TODO money check

        var regenerationPotion = new RegenerationPotion();
        regenerationPotion.sprite = regenerationPotionSprite;

        if (Player.Instance.gold >= RegenPotionPrice)
        {
            Player.Instance.AddPotion(regenerationPotion);
            Player.Instance.gold -= RegenPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

}
