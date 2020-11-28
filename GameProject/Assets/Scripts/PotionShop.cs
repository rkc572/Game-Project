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

    private const int HealthPotionPrice = 20;
    private const int ManaPotionPrice = 20;
    private const int RegenPotionPrice = 20;



    /* Used for testing
    void Awake()
    {
        totalGoldAmount = Player.Instance.gold;
    }

    void Start()
    {
        Debug.Log("Total Gold Count On Entry: " + player.Instance.);
    }
    */

    //Shouldn't need to deal w/ any negative value cases
    public void SellHealthPotion()
    { 
        var healthPotion = new HealthPotion(); 
        healthPotion.sprite = healthPotionSprite;

        if(Player.Instance.gold >= HealthPotionPrice)
        {
            Player.Instance.AddPotion(healthPotion);
            //totalGoldAmount -= HealthPotionPrice;
            Player.Instance.gold -= HealthPotionPrice;
            Debug.Log("New Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
        
    }

    public void SellManaPotion()
    {
        var manaPotion = new ManaPotion();
        manaPotion.sprite = manaPotionSprite;

        if (Player.Instance.gold >= ManaPotionPrice)
        {
            Player.Instance.AddPotion(manaPotion);
            //totalGoldAmount -= ManaPotionPrice;
            Player.Instance.gold -= ManaPotionPrice;
            Debug.Log("New Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public void SellRegenerationPotion()
    {
         var regenerationPotion = new RegenerationPotion();
        regenerationPotion.sprite = regenerationPotionSprite;

        if (Player.Instance.gold >= RegenPotionPrice)
        {
            Player.Instance.AddPotion(regenerationPotion);
            //totalGoldAmount -= RegenPotionPrice;
            Player.Instance.gold -= RegenPotionPrice;
            Debug.Log("New Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

}
