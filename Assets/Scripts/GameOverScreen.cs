using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public void GameOver(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void RespawnButton(){
        if (GameObject.Find("level"))
        {
            GameObject.Find("level").transform.position = new Vector3(0,-100,0);
            Destroy(GameObject.Find("level"));
        }
        if(GameObject.Find("boss"))
        {
            Destroy(GameObject.Find("boss"));
        }
        Destroy(GameObject.Find("Character"));
        if(GameStatistics.level >= 6){
            GameStatistics.checkpoint = true;
            GameStatistics.level = 6;
        }
        else{
            GameStatistics.level = 0;
        }
        UpgradeMenu.SetActive(true);
        GameObject.Find("Generate").GetComponent<Generate>().CreateRooms();
    }
}
