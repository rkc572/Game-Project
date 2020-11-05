using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameSceneManager sceneManager;
    public string scene;

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            sceneManager.LoadNextScene(scene);
        }
    }

    void PickRandomScene(string[] sceneList)
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(0, sceneList.Length);
        scene = sceneList[num];
    }

    private void Awake()
    {
        if (scene == "Random")
        {
            PickRandomScene(new string[] { "Forest scene 3", "Forest scene 4", "Forest scene 5" });
        }
    }
}
