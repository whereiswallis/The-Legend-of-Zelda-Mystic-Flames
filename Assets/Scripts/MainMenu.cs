using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TutorialOption;
    public GameObject SoundMenu;
    public GameObject Menu;
    public static bool isNew = false;
    public void StartGame()
    {
        if(isNew)
        {
            LevelChanger.levelToLoad = 2;
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
        }
        else
        {
            isNew = true;
            Menu.SetActive(false);
            TutorialOption.SetActive(true);
        }
    }

    public void SoundSetting()
    {
        Menu.SetActive(false);
        SoundMenu.SetActive(true);
    }

    public void QuidGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
