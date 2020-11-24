using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionSelect : MonoBehaviour
{
    Player player;

    LinkedListNode<Potion> currPotion;

    public Image selectedIMG;
    public Image nextIMG;
    public Image previousIMG;
    public Sprite[] potionSprites = new Sprite[6];
    public TextMeshProUGUI potionCount;

    void Start()
    {
        player = Player.Instance;
        currPotion = player.potions.First;
    }
    
    void Update()
    {
        if (currPotion != null)
        {
            player.selectedPotion = currPotion.Value;
            potionCount.text = player.selectedPotion.count.ToString();

            if (player.selectedPotion.count <= 0)
            {
                if (player.potions.Count > 1)
                {
                    LinkedListNode<Potion> next = currPotion.Next;
                    player.potions.Remove(currPotion);
                    currPotion = next;
                }
                else
                {
                    player.potions.Remove(currPotion);
                    currPotion = null;
                }
            }

            if (player.potions.First == player.potions.Last) // If only one potion left
            {
                nextIMG.enabled = false;
                previousIMG.enabled = false;
            }
            else
            {
                nextIMG.enabled = true;
                previousIMG.enabled = true;
            }

            if (player.potions.Count == 0) // If no potions left
            {
                selectedIMG.enabled = false;
                potionCount.enabled = false;
            }
            else
            {
                selectedIMG.enabled = true;
                potionCount.enabled = true;

            }

            if (Input.GetKeyDown("f"))
            {
                player.selectedPotion.UsePotion();
            }
            if (Input.GetKeyDown("e"))
            {
                currPotion = currPotion.Next ?? player.potions.First;
            }
            if (Input.GetKeyDown("q"))
            {
                currPotion = currPotion.Previous ?? player.potions.Last;
            }

            switch (player.selectedPotion.type)
            {
                case "health":
                    selectedIMG.sprite = potionSprites[0];
                    break;
                case "mana":
                    selectedIMG.sprite = potionSprites[1];
                    break;
                case "strength":
                    selectedIMG.sprite = potionSprites[2];
                    break;
                case "magboost":
                    selectedIMG.sprite = potionSprites[3];
                    break;
                case "speed":
                    selectedIMG.sprite = potionSprites[4];
                    break;
                case "regen":
                    selectedIMG.sprite = potionSprites[5];
                    break;
            }
            switch ((currPotion.Next ?? player.potions.First).Value.type)
            {
                case "health":
                    nextIMG.sprite = potionSprites[0];
                    break;
                case "mana":
                    nextIMG.sprite = potionSprites[1];
                    break;
                case "strength":
                    nextIMG.sprite = potionSprites[2];
                    break;
                case "magboost":
                    nextIMG.sprite = potionSprites[3];
                    break;
                case "speed":
                    nextIMG.sprite = potionSprites[4];
                    break;
                case "regen":
                    nextIMG.sprite = potionSprites[5];
                    break;
            }
            switch ((currPotion.Previous ?? player.potions.Last).Value.type)
            {
                case "health":
                    previousIMG.sprite = potionSprites[0];
                    break;
                case "mana":
                    previousIMG.sprite = potionSprites[1];
                    break;
                case "strength":
                    previousIMG.sprite = potionSprites[2];
                    break;
                case "magboost":
                    previousIMG.sprite = potionSprites[3];
                    break;
                case "speed":
                    previousIMG.sprite = potionSprites[4];
                    break;
                case "regen":
                    previousIMG.sprite = potionSprites[5];
                    break;
            }
        }
    }
}
