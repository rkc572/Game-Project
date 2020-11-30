using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public Canvas ui;
    public ParticleSystem entity;
    public int currEntity;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI itemOne;
    public TextMeshProUGUI itemTwo;
    public TextMeshProUGUI descriptionOne;
    public TextMeshProUGUI descriptionTwo;
    public PlayerItem[] upgradables = new PlayerItem[2];
    public ElementalAttribute currElement;

    void Awake()
    {
        switch (currEntity)
        {
            case 0:
                dialogue.outlineWidth = 0.35f;
                dialogue.outlineColor = new Color32(255, 76, 76, 255);
                currElement = ElementalAttribute.FIRE;
                break;
            case 1:
                dialogue.outlineWidth = 0.35f;
                dialogue.outlineColor = new Color32(0, 200, 255, 255);
                currElement = ElementalAttribute.WATER;
                break;
            case 2:
                dialogue.outlineWidth = 0.35f;
                dialogue.outlineColor = new Color32(128, 128, 128, 255);
                currElement = ElementalAttribute.AIR;
                break;
            case 3:
                dialogue.outlineWidth = 0.35f;
                dialogue.outlineColor = new Color32(87, 58, 33, 255);
                currElement = ElementalAttribute.EARTH;
                break;
        }
    }
    
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

        if (upgradables[0] == Player.Instance.playerSword)
        {
            itemOne.text = "Upgrade SWORD";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionOne.text = "Hold SPACE while using the SWORD to perform a fire swing, setting enemies hit on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionOne.text = "Hold SPACE while using the SWORD to perform a water swing, healing you for a portion of damage dealt.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionOne.text = "Hold SPACE while using the SWORD to perform an air swing, extending your sword's range and blowing enemies hit away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionOne.text = "Hold SPACE while using the SWORD to perform an earth swing, stunning enemies hit.";
                    break;
            }
        }
        else if (upgradables[0] == Player.Instance.playerMagicStaff)
        {
            itemOne.text = "Upgrade\nMAGIC STAFF";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionOne.text = "Hold SPACE while using the MAGIC STAFF to shoot a fire bolt, setting enemies hit on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionOne.text = "Hold SPACE while using the MAGIC STAFF to shoot an ice bolt, freezing enemies hit.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionOne.text = "Hold SPACE while using the MAGIC STAFF to shoot an air bolt, blowing enemies hit away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionOne.text = "Hold SPACE while using the MAGIC STAFF to shoot an earth bolt, stunning enemies hit.";
                    break;
            }
        }
        else if (upgradables[0] == Player.Instance.playerEtherealPendant)
        {
            itemOne.text = "Upgrade\nETHEREAL PENDANT";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionOne.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the fire realm, setting enemies you pass through on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionOne.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the water realm, stealing health from enemies you pass through.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionOne.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the wind realm, temporarily increasing your movement speed while slowing enemies you pass through.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionOne.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the earth realm, temporarily stealing defense from enemies you pass through.";
                    break;
            }
        }
        else if (upgradables[0] == Player.Instance.playerDashBoots)
        {
            itemOne.text = "Upgrade\nDASH BOOTS";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionOne.text = "Hold SPACE while using the DASH BOOTS to perform a fire dash, leaving a trail of fire that burns enemies in your path.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionOne.text = "Hold SPACE while using the DASH BOOTS to perform a water dash, leaving a trail of ice that freezes enemies in your path.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionOne.text = "Hold SPACE while using the DASH BOOTS to perform a nearly instant air dash, temporarily increasing your movement speed.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionOne.text = "Hold SPACE while using the DASH BOOTS to perform an earth dash, increasing your defense and dealing damage to enemies you charge into while knocking them back.";
                    break;
            }
        }
        else if (upgradables[0] == Player.Instance.playerShield)
        {
            itemOne.text = "Upgrade SHIELD";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionOne.text = "Hold SPACE while activating the SHIELD to perform a fire bash, releasing a burst of flames around you that burns enemies.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionOne.text = "Hold SPACE while activating the SHIELD to perform an ice bash, releasing a burst of ice around you that freezes enemies.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionOne.text = "Hold SPACE while activating the SHIELD to perform an air bash, blowing enemies and projectiles away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionOne.text = "Hold SPACE while activating the SHIELD to use the earth shield, extending the shield's size to cover your sides.";
                    break;
            }
        }

        if (upgradables[1] == Player.Instance.playerSword)
        {
            itemTwo.text = "Upgrade SWORD";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionTwo.text = "Hold SPACE while using the SWORD to perform a fire swing, setting enemies hit on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionTwo.text = "Hold SPACE while using the SWORD to perform a water swing, healing you for a portion of damage dealt.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionTwo.text = "Hold SPACE while using the SWORD to perform an air swing, extending your sword's range and blowing enemies hit away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionTwo.text = "Hold SPACE while using the SWORD to perform an earth swing, stunning enemies hit.";
                    break;
            }
        }
        else if (upgradables[1] == Player.Instance.playerMagicStaff)
        {
            itemTwo.text = "Upgrade\nMAGIC STAFF";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionTwo.text = "Hold SPACE while using the MAGIC STAFF to shoot a fire bolt, setting enemies hit on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionTwo.text = "Hold SPACE while using the MAGIC STAFF to shoot an ice bolt, freezing enemies hit.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionTwo.text = "Hold SPACE while using the MAGIC STAFF to shoot an air bolt, blowing enemies hit away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionTwo.text = "Hold SPACE while using the MAGIC STAFF to shoot an earth bolt, stunning enemies hit.";
                    break;
            }
        }
        else if (upgradables[1] == Player.Instance.playerEtherealPendant)
        {
            itemTwo.text = "Upgrade\nETHEREAL PENDANT";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionTwo.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the fire realm, setting enemies you pass through on fire.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionTwo.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the water realm, stealing health from enemies you pass through.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionTwo.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the wind realm, temporarily increasing your movement speed while slowing enemies you pass through.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionTwo.text = "Hold SPACE while activating the ETHEREAL PENDANT to enter the earth realm, temporarily stealing defense from enemies you pass through.";
                    break;
            }
        }
        else if (upgradables[1] == Player.Instance.playerDashBoots)
        {
            itemTwo.text = "Upgrade\nDASH BOOTS";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionTwo.text = "Hold SPACE while using the DASH BOOTS to perform a fire dash, leaving a trail of fire that burns enemies in your path.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionTwo.text = "Hold SPACE while using the DASH BOOTS to perform a water dash, leaving a trail of ice that freezes enemies in your path.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionTwo.text = "Hold SPACE while using the DASH BOOTS to perform a nearly instant air dash, temporarily increasing your movement speed.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionTwo.text = "Hold SPACE while using the DASH BOOTS to perform an earth dash, increasing your defense and dealing damage to enemies you charge into while knocking them back.";
                    break;
            }
        }
        else if (upgradables[1] == Player.Instance.playerShield)
        {
            itemTwo.text = "Upgrade SHIELD";
            switch (currElement)
            {
                case ElementalAttribute.FIRE:
                    descriptionTwo.text = "Hold SPACE while activating the SHIELD to perform a fire bash, releasing a burst of flames around you that burns enemies.";
                    break;
                case ElementalAttribute.WATER:
                    descriptionTwo.text = "Hold SPACE while activating the SHIELD to perform an ice bash, releasing a burst of ice around you that freezes enemies.";
                    break;
                case ElementalAttribute.AIR:
                    descriptionTwo.text = "Hold SPACE while activating the SHIELD to perform an air bash, blowing enemies and projectiles away.";
                    break;
                case ElementalAttribute.EARTH:
                    descriptionTwo.text = "Hold SPACE while activating the SHIELD to use the earth shield, extending the shield's size to cover your sides.";
                    break;
            }
        }
    }

    public void upgradeOne()
    {
        upgradables[0].elementalAttribute = currElement;
        Player.Instance.hasUpgrades[currEntity] = true;

        ui.gameObject.SetActive(false);
        Player.Instance.inputController.detectInput = true;
        var main = entity.main;
        main.loop = false;
    }

    public void upgradeTwo()
    {
        upgradables[1].elementalAttribute = currElement;
        Player.Instance.hasUpgrades[currEntity] = true;

        ui.gameObject.SetActive(false);
        Player.Instance.inputController.detectInput = true;
        var main = entity.main;
        main.loop = false;
    }
}
