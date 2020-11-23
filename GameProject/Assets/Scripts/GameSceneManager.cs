using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public bool deathSceneActive = false;

    public AudioClip deathMusic;


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

    public IEnumerator PlayerDied(Player player)
    {
        AudioManager.Instance.musicSource.loop = false;
        AudioManager.Instance.PlayMusic(deathMusic);

        deathSceneActive = true;
        player.inputController.detectInput = false;

        player.movementController.StopMoving();

        // kill all non player mobs
        var mobs = FindObjectsOfType<Mob>();
        foreach (Mob mob in mobs) {
            if (mob.GetComponent<Player>() == null)
            {
                Destroy(mob.transform.parent.gameObject);
            }
        }

        // zoom in on player
        var camera = FindObjectOfType<Camera>();
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        camera.orthographicSize = 0.5f;

        yield return new WaitForSeconds(1f);

        // trigger player death animation
        player.animator.SetBool("Dead", true);

        yield return new WaitForSeconds(2f);


        Image faderImage = GameObject.Find("Fader").GetComponent<Image>();

        // fade screen
        for (int i = 0; i <= 100; i++)
        {
            faderImage.color = new Color(0, 0, 0, (float)i / 100.0f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1.0f);

        camera.orthographicSize = 1.3f;

        var currentSceneName = SceneManager.GetActiveScene().name;
        LoadSceneMovePlayer(currentSceneName, player.lastRecordedPosition);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator SceneFadeTransition(string sceneName, Vector3 pos)
    {

        var player = Player.Instance;

        player.inputController.detectInput = false;

        player.movementController.StopMoving();

        Image faderImage = GameObject.Find("Fader").GetComponent<Image>();
        //FADE IN

        for (int i = 0; i < 100; i++)
        {
            faderImage.color = new Color(0, 0, 0, Mathf.Min(1, faderImage.color.a + +0.01f));
            yield return new WaitForSeconds(0.01f);
        }

        player.inputController.detectInput = false;
        player.lastRecordedPosition = pos;

        if (!deathSceneActive)
        {
            player.lastRecordedHealth = player.health;
            player.lastRecordedMana = player.mana;
        }

        player.health = player.lastRecordedHealth;
        player.mana = player.lastRecordedMana;

        player.transform.parent.position = pos;
        player.inputController.detectInput = true;
        player.animator.SetBool("Dead", false);

        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneMovePlayer(string sceneName, Vector3 pos)
    {
        var player = Player.Instance;

        player.inputController.detectInput = false;
        player.lastRecordedPosition = pos;

        if (!deathSceneActive)
        {
            player.lastRecordedHealth = player.health;
            player.lastRecordedMana = player.mana;
        }

        player.health = player.lastRecordedHealth;
        player.mana = player.lastRecordedMana;

        SceneManager.LoadScene(sceneName);
        player.transform.parent.position = pos;
        player.inputController.detectInput = true;
        player.animator.SetBool("Dead", false);
    }
}
