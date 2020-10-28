using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTester : MonoBehaviour
{
    // Add reference to script that the function you want to test belongs to
    public GameSceneManager test;
    
    void Update()
    {
        if (Input.GetKey("t")) // Press t to test function
        {
            // Create test params if necessary and call the function to be tested
            Vector3 position = new Vector3(0.5f, 0.5f, 0);
            test.LoadSceneMovePlayer("Sandbox 2", position);
        }
    }
}
