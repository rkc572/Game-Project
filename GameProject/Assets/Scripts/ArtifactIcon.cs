using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;

    public Sprite swordIcon;
    public Sprite fireSwordIcon;
    public Sprite waterSwordIcon;
    public Sprite windSwordIcon;
    public Sprite earthSwordIcon;


    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.sword.elementalAttribute)
        {
            case ElementalAttribute.None:
                icon.sprite = swordIcon;
                break;
            case ElementalAttribute.Earth:
                icon.sprite = earthSwordIcon;
                break;
            case ElementalAttribute.Fire:
                icon.sprite = fireSwordIcon;
                break;
            case ElementalAttribute.Water:
                icon.sprite = waterSwordIcon;
                break;
            case ElementalAttribute.Wind:
                icon.sprite = windSwordIcon;
                break;
        }
    }
}
