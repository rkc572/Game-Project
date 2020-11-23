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
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.playerSword.elementalAttribute)
        {
            case ElementalAttribute.NONE:
                icon.sprite = swordIcon;
                break;
            case ElementalAttribute.EARTH:
                icon.sprite = earthSwordIcon;
                break;
            case ElementalAttribute.FIRE:
                icon.sprite = fireSwordIcon;
                break;
            case ElementalAttribute.WATER:
                icon.sprite = waterSwordIcon;
                break;
            case ElementalAttribute.AIR:
                icon.sprite = windSwordIcon;
                break;
        }
    }
}
