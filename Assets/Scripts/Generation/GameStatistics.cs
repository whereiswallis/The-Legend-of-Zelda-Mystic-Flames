using System;
using UnityEngine;
using TMPro;

public class GameStatistics : MonoBehaviour
{
    public static int final = 10;
    [SerializeField]
    public static int level = 0;

    public static float Timer = 0;

    public static int attackLevel = 0;

    public static int goldLevel = 0;

    public static int defenceLevel = 0;

    public static int healthLevel = 0;

    public static int speedLevel = 0;
    public static bool checkpoint = false;
    public GameObject congrats;

    void Start(){
        congrats = GameObject.Find("Congrats");
        congrats.SetActive(false);
    }

    void Update()
    {
        Timer += Time.deltaTime;
    }

    public void convertion()
    {
        TimeSpan time = TimeSpan.FromSeconds(Timer);
        congrats.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Time:" + time.ToString("hh':'mm':'ss");
        congrats.SetActive(true);
        Time.timeScale = 0;
    }


}
