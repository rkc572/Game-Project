using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public Canvas ui;
    public int currEntity;
    public Image elementIcon;
    public Sprite fire, water, air, earth;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI itemOne;
    public TextMeshProUGUI itemTwo;
    public PlayerItem[] upgradables = new PlayerItem[2];
    public ElementalAttribute currElement;

    void Awake()
    {
        dialogue.text = "Thank you for releasing me. In return I shall grant you the power of ";
        switch (currEntity)
        {
            case 0:
                dialogue.outlineWidth = 0.2f;
                dialogue.outlineColor = new Color32(255, 76, 76, 255);
                dialogue.text += "FIRE";
                elementIcon.sprite = fire;
                currElement = ElementalAttribute.FIRE;
                break;
            case 1:
                dialogue.outlineWidth = 0.2f;
                dialogue.outlineColor = new Color32(0, 200, 255, 255);
                dialogue.text += "WATER";
                elementIcon.sprite = water;
                currElement = ElementalAttribute.WATER;
                break;
            case 2:
                dialogue.outlineWidth = 0.2f;
                dialogue.outlineColor = new Color32(128, 128, 128, 255);
                dialogue.text += "AIR";
                elementIcon.sprite = air;
                currElement = ElementalAttribute.AIR;
                break;
            case 3:
                dialogue.outlineWidth = 0.2f;
                dialogue.outlineColor = new Color32(87, 58, 33, 255);
                dialogue.text += "EARTH";
                elementIcon.sprite = earth;
                currElement = ElementalAttribute.EARTH;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.playerSword.elementalAttribute == ElementalAttribute.NONE)
        {
            upgradables[0] = Player.Instance.playerSword;

            foreach (PlayerItem playerItem in Player.Instance.artifacts)
            {
                if (playerItem.elementalAttribute == ElementalAttribute.NONE)
                {
                    upgradables[1] = playerItem;
                    break;
                }
            }
        }
        else
        {
            int i = 0;
            foreach (PlayerItem playerItem in Player.Instance.artifacts)
            {
                if (playerItem.elementalAttribute == ElementalAttribute.NONE)
                {
                    upgradables[i] = playerItem;
                    i++;
                }
                if (i == 2)
                    break;
            }
        }
    }

    public void upgradeOne()
    {
        upgradables[0].elementalAttribute = currElement;
        Player.Instance.hasUpgrades[currEntity] = true;
        ui.gameObject.SetActive(false);
    }

    public void upgradeTwo()
    {
        upgradables[1].elementalAttribute = currElement;
        Player.Instance.hasUpgrades[currEntity] = true;
        ui.gameObject.SetActive(false);
    }
}
