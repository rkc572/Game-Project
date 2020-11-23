using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicStaff : PlayerItem
{

    public void Magic()
    {

    }


    public void ElementalMagic()
    {

    }

    public override void DetectInput()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ElementalMagic();
            }
            else
            {
                Magic();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
