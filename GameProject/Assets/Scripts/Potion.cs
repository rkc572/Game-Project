using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string type; // health, mana, strength, magboost, speed, regen
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Potion (string t, int c)
    {
        this.type = t;
        this.count = c;
    }

    public void UsePotion()
    {
        // grant effect depending on potion type
        //switch (this.type)
        //{
        //    case "health":
        //        break;
        //    case "mana":
        //        break;
        //    case "strength":
        //        break;
        //    case "magboost":
        //        break;
        //    case "speed":
        //        break;
        //    case "regen":
        //        break;
        //}
        this.count--;
        Debug.Log("use potion");
    }
}
