using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject SoundSetting;
    private int currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SoundSetting = GameObject.Find("SoundMenu");
        SoundSetting.SetActive(false);
        if(currentScene == 0)
        {
            Menu = GameObject.Find("MainMenu");
        }
        else
        {
            Menu = GameObject.Find("PauseMenu");
            Menu.SetActive(false);
        }
    }

    public void SetVolumn(float volumn)
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().volume = volumn;
    }
    public void BackButton()
    {
        Menu.SetActive(true);
        SoundSetting.SetActive(false);
    }
}
