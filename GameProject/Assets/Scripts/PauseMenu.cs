using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButtons;
    public GameObject pauseMenuOptions;
    Player player;

    // Start is called before the first frame update
    void Awake()
    {
        pauseMenuUI.SetActive(false);
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = Player.Instance;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        player.inputController.detectInput = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        player.inputController.detectInput = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadOptions()
    {
        pauseMenuButtons.SetActive(false);
        pauseMenuOptions.SetActive(true);
    }

    public void UnLoadOptions()
    {
        pauseMenuButtons.SetActive(true);
        pauseMenuOptions.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Destroy(player.transform.parent.gameObject);
        GameSceneManager.Instance.LoadNextScene("StartMenu");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }
}
