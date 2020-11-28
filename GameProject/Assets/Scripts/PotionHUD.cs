using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionHUD : MonoBehaviour
{

    public Image leftPotionImage, middlePotionImage, rightPotionImage;
    public TextMeshProUGUI potCount;

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.selectedPotion != null)
            potCount.text = Player.Instance.selectedPotion.quantity.ToString();
        else
            potCount.text = null;

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

        if (leftPotionImage.sprite != null)
            leftPotionImage.color = Color.white;
        else
            leftPotionImage.color = new Color32(55, 55, 55, 100);
        if (middlePotionImage.sprite != null)
            middlePotionImage.color = Color.white;
        else
            middlePotionImage.color = new Color32(55, 55, 55, 100);

        if (rightPotionImage.sprite != null)
            rightPotionImage.color = Color.white;
        else
            rightPotionImage.color = new Color32(55, 55, 55, 100);

    }
}
