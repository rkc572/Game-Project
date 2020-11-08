using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCount : MonoBehaviour
{
    Player player;
    public TextMeshProUGUI goldCount;

    void Awake()
    {
        goldCount.outlineWidth = 0.2f;
        goldCount.outlineColor = new Color32(255, 128, 0, 255);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        goldCount.text = player.gold.ToString("R");
    }
}
