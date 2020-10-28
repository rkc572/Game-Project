using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Singleton
    public static GameSceneManager Instance = null;

    public GameObject player;

    private void Awake()
    {
        Instance = this;

        if (SceneManager.GetActiveScene().name == "TeamSplashScreen")
        {
            Invoke("loadStartMenu", 4f);
        }

        DontDestroyOnLoad(gameObject);
    }

    void loadStartMenu()
    {
        LoadNextScene("StartMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneMovePlayer(string sceneName, Vector3 pos)
    {
        SceneManager.LoadScene(sceneName);
        player.transform.position = pos;
    }
}
