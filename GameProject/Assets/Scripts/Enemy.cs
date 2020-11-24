using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : Mob
{
    public bool attackBlocked = false;

    bool droppedItem = false;
    public SortingGroup sortingGroup;
    public List<GameObject> itemDrops;

    void DropItem()
    {
        Instantiate(itemDrops[Random.Range(0, itemDrops.Count)], transform.position, Quaternion.identity);
        droppedItem = true;
    }

    protected override void Update()
    {
        base.Update();

        if (!droppedItem && health <= 0.0f)
        {
            DropItem();
        }
    }
}
