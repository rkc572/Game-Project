using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Singleton
    public static GameSceneManager Instance = null;
    
    private void Awake()
    {
        Instance = this;

        if (SceneManager.GetActiveScene().name == "TeamSplashScreen") 
        {
            Invoke("loadStartMenu", 4f);
        }
    }

    void loadStartMenu()
    {
        LoadNextScene("StartMenu");
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
