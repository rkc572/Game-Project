using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    
    public Sprite healthPotionSprite,
        manaPotionSprite,
        regenerationPotionSprite,
        speedPotionSprite,
        magicPotionSprite,
        strengthPotionSprite;

    public const int HealthPotionPrice = 30;
    public const int ManaPotionPrice = 30;
    public const int RegenPotionPrice = 20;
    public const int strengthPotionPrice = 20;
    public const int magicPotionPrice = 20;
    public const int speedPotionPrice = 20;


    void Start()
    {
        Debug.Log("Total Gold Count On Entry: " + Player.Instance.gold);
    }
   
    public void SellHealthPotion()
    {

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

    public void SellSpeedBoostPotion()
    {

        var speedPotion = new SpeedBoostPotion();
        speedPotion.sprite = speedPotionSprite;

        if (Player.Instance.gold >= speedPotionPrice)
        {
            Player.Instance.AddPotion(speedPotion);
            Player.Instance.gold -= speedPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public void SellMagicBoostPotion()
    {

        var magicPotion = new MagicBoostPotion();
        magicPotion.sprite = magicPotionSprite;

        if (Player.Instance.gold >= magicPotionPrice)
        {
            Player.Instance.AddPotion(magicPotion);
            Player.Instance.gold -= magicPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public void SellStrengthPotion()
    {

        var strengthPotion = new StrengthPotion();
        strengthPotion.sprite = strengthPotionSprite;

        if (Player.Instance.gold >= strengthPotionPrice)
        {
            Player.Instance.AddPotion(strengthPotion);
            Player.Instance.gold -= strengthPotionPrice;
            Debug.Log("Gold Count: " + Player.Instance.gold);
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

}
