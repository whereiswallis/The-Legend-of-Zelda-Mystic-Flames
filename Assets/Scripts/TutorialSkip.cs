using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    public void Yes()
    {
        MainMenu.isNew = true;
        LevelChanger.levelToLoad = 2;
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
    }
    public void No()
    {
        MainMenu.isNew = true;
        LevelChanger.levelToLoad = 1;
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
    }
}
