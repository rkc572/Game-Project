using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameSceneManager sceneManager;
    public string scene;
    public Vector3 location;

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            sceneManager.LoadSceneMovePlayer(scene, location);
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
