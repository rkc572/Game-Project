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

        if (Player.Instance.potions.Count > 1)
        {
            leftPotionImage.sprite = Player.Instance.potions[Mathf.Min((Player.Instance.potions.Count / 2) - 1, Player.Instance.potions.Count - 1)].sprite;
            middlePotionImage.sprite = Player.Instance.potions[Mathf.Min((Player.Instance.potions.Count / 2), Player.Instance.potions.Count - 1)].sprite;
            rightPotionImage.sprite = Player.Instance.potions[Mathf.Min((Player.Instance.potions.Count / 2) + 1, Player.Instance.potions.Count - 1)].sprite;
        }

    }
}
