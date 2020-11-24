using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionHUD : MonoBehaviour
{

    public Image leftPotionImage, middlePotionImage, rightPotionImage;


    // Update is called once per frame
    void Update()
    {
        //TODO IMPLEMENT CORRECTLY
        leftPotionImage.sprite = Player.Instance.selectedPotion.sprite;
        middlePotionImage.sprite = Player.Instance.selectedPotion.sprite;
        rightPotionImage.sprite = Player.Instance.selectedPotion.sprite;

    }
}
