using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUD : MonoBehaviour
{
    public Image selected, one, two, three, four;
    public Sprite staff, boots, pendant, shield;
    public GameObject highlight;

    void Update()
    {
        // Selected
        if (Player.Instance.selectedArtifact != null)
        {
            highlight.SetActive(true);
            selected.color = Color.white;

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
            one.color = Color.white;

            if (Player.Instance.artifacts[0] == Player.Instance.playerMagicStaff)
                one.sprite = staff; 

            else if (Player.Instance.artifacts[0] == Player.Instance.playerDashBoots)
                one.sprite = boots;

            else if (Player.Instance.artifacts[0] == Player.Instance.playerEtherealPendant)
                one.sprite = pendant;

            else if (Player.Instance.artifacts[0] == Player.Instance.playerShield)
                one.sprite = shield;
        }

        // Second slot
        if (Player.Instance.artifacts.Count >= 2)
        {
            two.color = Color.white;

            if (Player.Instance.artifacts[1] == Player.Instance.playerMagicStaff)
                two.sprite = staff;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerDashBoots)
                two.sprite = boots;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerEtherealPendant)
                two.sprite = pendant;

            else if (Player.Instance.artifacts[1] == Player.Instance.playerShield)
                two.sprite = shield;
        }

        // Thrid slot
        if (Player.Instance.artifacts.Count >= 3)
        {
            three.color = Color.white;

            if (Player.Instance.artifacts[2] == Player.Instance.playerMagicStaff)
                three.sprite = staff;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerDashBoots)
                three.sprite = boots;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerEtherealPendant)
                three.sprite = pendant;

            else if (Player.Instance.artifacts[2] == Player.Instance.playerShield)
                three.sprite = shield;
        }

        // Fourth slot
        if (Player.Instance.artifacts.Count >= 4)
        {
            four.color = Color.white;

            if (Player.Instance.artifacts[3] == Player.Instance.playerMagicStaff)
                four.sprite = staff;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerDashBoots)
                four.sprite = boots;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerEtherealPendant)
                four.sprite = pendant;

            else if (Player.Instance.artifacts[3] == Player.Instance.playerShield)
                four.sprite = shield;
        }
    }
}
