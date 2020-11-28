using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUD : MonoBehaviour
{
    public Image selected, one, two, three, four;
    public CanvasGroup alphaSelected, alphaOne, alphaTwo, alphaThree, alphaFour;
    public Sprite staff, boots, pendant, shield;
    public GameObject highlight;

    public Image iconOne, iconTwo, iconThree, iconFour;
    public Sprite fire, water, air, earth;

    void Update()
    {
        // Selected
        if (Player.Instance.selectedArtifact != null)
        {
            highlight.SetActive(true);
            alphaSelected.alpha = 1;

            if (Player.Instance.selectedArtifact == Player.Instance.playerMagicStaff)
                selected.sprite = staff;

            else if (Player.Instance.selectedArtifact == Player.Instance.playerDashBoots)
                selected.sprite = boots;

            else if (Player.Instance.selectedArtifact == Player.Instance.playerEtherealPendant)
                selected.sprite = pendant;

            else if (Player.Instance.selectedArtifact == Player.Instance.playerShield)
                selected.sprite = shield;

            // Set highlight
            if (Player.Instance.selectedArtifact == Player.Instance.artifacts[0])
                highlight.transform.position = one.gameObject.transform.position;

            else if (Player.Instance.selectedArtifact == Player.Instance.artifacts[1])
                highlight.transform.position = two.gameObject.transform.position;

            else if (Player.Instance.selectedArtifact == Player.Instance.artifacts[2])
                highlight.transform.position = three.gameObject.transform.position;

            else if (Player.Instance.selectedArtifact == Player.Instance.artifacts[3])
                highlight.transform.position = four.gameObject.transform.position;
        }

        // First slot
        if (Player.Instance.artifacts.Count >= 1)
        {
            alphaOne.alpha = 1;

            if (Player.Instance.artifacts[0] == Player.Instance.playerMagicStaff)
                one.sprite = staff; 

            else if (Player.Instance.artifacts[0] == Player.Instance.playerDashBoots)
                one.sprite = boots;

            else if (Player.Instance.artifacts[0] == Player.Instance.playerEtherealPendant)
                one.sprite = pendant;

            else if (Player.Instance.artifacts[0] == Player.Instance.playerShield)
                one.sprite = shield;

            // Element icon slot one
            if (Player.Instance.artifacts[0].elementalAttribute != ElementalAttribute.NONE)
            {
                iconOne.gameObject.SetActive(true);

                switch (Player.Instance.artifacts[0].elementalAttribute)
                {
                    case ElementalAttribute.FIRE:
                        iconOne.sprite = fire;
                        break;
                    case ElementalAttribute.WATER:
                        iconOne.sprite = water;
                        break;
                    case ElementalAttribute.AIR:
                        iconOne.sprite = air;
                        break;
                    case ElementalAttribute.EARTH:
                        iconOne.sprite = earth;
                        break;
                }
            }
        }

        // Second slot
        if (Player.Instance.artifacts.Count >= 2)
        {
            alphaTwo.alpha = 1;

            if (Player.Instance.artifacts[1] == Player.Instance.playerMagicStaff)
                two.sprite = staff;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerDashBoots)
                two.sprite = boots;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerEtherealPendant)
                two.sprite = pendant;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerShield)
                two.sprite = shield;

            // Element icon slot two
            if (Player.Instance.artifacts[1].elementalAttribute != ElementalAttribute.NONE)
            {
                iconTwo.gameObject.SetActive(true);

                switch (Player.Instance.artifacts[1].elementalAttribute)
                {
                    case ElementalAttribute.FIRE:
                        iconTwo.sprite = fire;
                        break;
                    case ElementalAttribute.WATER:
                        iconTwo.sprite = water;
                        break;
                    case ElementalAttribute.AIR:
                        iconTwo.sprite = air;
                        break;
                    case ElementalAttribute.EARTH:
                        iconTwo.sprite = earth;
                        break;
                }
            }
        }

        // Thrid slot
        if (Player.Instance.artifacts.Count >= 3)
        {
            alphaThree.alpha = 1;

            if (Player.Instance.artifacts[2] == Player.Instance.playerMagicStaff)
                three.sprite = staff;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerDashBoots)
                three.sprite = boots;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerEtherealPendant)
                three.sprite = pendant;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerShield)
                three.sprite = shield;

            // Element icon slot three
            if (Player.Instance.artifacts[2].elementalAttribute != ElementalAttribute.NONE)
            {
                iconThree.gameObject.SetActive(true);

                switch (Player.Instance.artifacts[2].elementalAttribute)
                {
                    case ElementalAttribute.FIRE:
                        iconThree.sprite = fire;
                        break;
                    case ElementalAttribute.WATER:
                        iconThree.sprite = water;
                        break;
                    case ElementalAttribute.AIR:
                        iconThree.sprite = air;
                        break;
                    case ElementalAttribute.EARTH:
                        iconThree.sprite = earth;
                        break;
                }
            }
        }

        // Fourth slot
        if (Player.Instance.artifacts.Count >= 4)
        {
            alphaFour.alpha = 1;

            if (Player.Instance.artifacts[3] == Player.Instance.playerMagicStaff)
                four.sprite = staff;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerDashBoots)
                four.sprite = boots;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerEtherealPendant)
                four.sprite = pendant;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerShield)
                four.sprite = shield;

            // Element icon slot four
            if (Player.Instance.artifacts[3].elementalAttribute != ElementalAttribute.NONE)
            {
                iconFour.gameObject.SetActive(true);

                switch (Player.Instance.artifacts[3].elementalAttribute)
                {
                    case ElementalAttribute.FIRE:
                        iconFour.sprite = fire;
                        break;
                    case ElementalAttribute.WATER:
                        iconFour.sprite = water;
                        break;
                    case ElementalAttribute.AIR:
                        iconFour.sprite = air;
                        break;
                    case ElementalAttribute.EARTH:
                        iconFour.sprite = earth;
                        break;
                }
            }
        }
    }
}
