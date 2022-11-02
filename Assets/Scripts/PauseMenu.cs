using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject soundMenu;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
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
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Sound()
    {
        soundMenu.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void LoadMenu()
    {
        LevelChanger.levelToLoad = 0;
        GameStatistics.level = 0;
        GameStatistics.attackLevel = 0;
        GameStatistics.goldLevel = 0;
        GameStatistics.defenceLevel = 0;
        GameStatistics.speedLevel = 0;
        GameStatistics.Timer = 0;
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
