using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PotionHUD : MonoBehaviour
{

    public Image leftPotionImage, middlePotionImage, rightPotionImage;


    // Update is called once per frame
    void Update()
    {

        if (Player.Instance.potions.Count >= 1)
        {
            var potionsListRange = Enumerable.Range(0, Player.Instance.potions.Count);

            var leftIndex = Player.Instance.potions.Count / 2 - 1;
            leftPotionImage.sprite = potionsListRange.Contains(leftIndex) ? Player.Instance.potions[leftIndex].sprite : null;

            middlePotionImage.sprite = Player.Instance.selectedPotion.sprite;

            var rightIndex = Player.Instance.potions.Count / 2 + 1;
            rightPotionImage.sprite = potionsListRange.Contains(rightIndex) ? Player.Instance.potions[rightIndex].sprite : null;

        }
        else
        {
            leftPotionImage.sprite = null;
            middlePotionImage.sprite = null;
            rightPotionImage.sprite = null;
        }

    }
}
