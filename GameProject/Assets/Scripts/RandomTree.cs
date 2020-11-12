using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTree : MonoBehaviour
{
    public SpriteRenderer referenceTree;
    public GameObject[] treeList;

    void ChooseTree()
    {
        int num = UnityEngine.Random.Range(0, 18);
        GameObject tree = Instantiate(treeList[num], transform.position, Quaternion.identity);

        tree.transform.localScale = new Vector3(new List<int> { -1, 1 }[Random.Range(0, 2)], 1.0f, 1.0f);

        tree.transform.parent = this.transform;

        referenceTree.enabled = false;
    }

void Start()
    {
        ChooseTree();
    }
}
