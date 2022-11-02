using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static Animator animator;
    private int currentLevel;
    public static int levelToLoad;
    public bool LevelObjective;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelToLoad = currentLevel;
        LevelObjective = false;
    }

    void Update()
    {
        if (Generate.canProceed)
        {
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
        }
    }


    
    public void Fade()
    {
        if(currentLevel != levelToLoad)
        {
            animator.SetTrigger("FadeOut");
            Invoke("LoadLevel", 1.5f);
        }
        if(currentLevel == 2)
        {
            animator.SetTrigger("FadeOut");
        }
    }
    

    void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
