using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    
    public Sprite healthPotionSprite,
        manaPotionSprite,
        regenerationPotionSprite;

    Player player;
    public float totalGoldAmount;

    public const int HealthPotionPrice = 50;
    public const int ManaPotionPrice = 50;
    public const int RegenPotionPrice = 50;

   
    void Awake()
    {
        totalGoldAmount = Player.Instance.gold;
    }

    void Start()
    {
        Debug.Log("Total Gold Count On Entry: " + totalGoldAmount);
    }
   
    public void SellHealthPotion()
    {
        //TODO money check

        var healthPotion = new HealthPotion(); 
        healthPotion.sprite = healthPotionSprite;

        if(totalGoldAmount >= HealthPotionPrice)
        {
            Player.Instance.AddPotion(healthPotion);
            totalGoldAmount -= HealthPotionPrice;
            Debug.Log("Gold Count: " + totalGoldAmount);
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


        if (totalGoldAmount >= ManaPotionPrice)
        {
            Player.Instance.AddPotion(manaPotion);
            totalGoldAmount -= ManaPotionPrice;
            Debug.Log("Gold Count: " + totalGoldAmount);
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

        if (totalGoldAmount >= RegenPotionPrice)
        {
            Player.Instance.AddPotion(regenerationPotion);
            totalGoldAmount -= RegenPotionPrice;
            Debug.Log("Gold Count: " + totalGoldAmount);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

}
