using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int player_max_health = 100;
    public int player_health;
    public int potion = 15;
    public GameObject player_healthbar;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        player_healthbar = GameObject.Find("Health Bar");
        gameOverScreen = GameObject.Find("Game Over");
        gameOverScreen.SetActive(false);
        player_health = player_max_health;
        player_healthbar.GetComponent<HealthBar>().SetMaxHealth(player_max_health);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            TakeDamage(20);
        }
        if(player_health <= 0){
            Respawn();
        }
    }

    void TakeDamage(int damage){
        player_health -= damage;
        player_healthbar.GetComponent<HealthBar>().setHealth(player_health);
    }

    public void PotionHeal()
    {
        player_health += potion;
        if(player_health > player_max_health)
        {
            player_health = player_max_health;
        }
        player_healthbar.GetComponent<HealthBar>().setHealth(player_health);
    }

    void Respawn(){
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().GameOver();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Potion" & gameObject.tag != "Weapon")
        {
            Destroy(other.gameObject);
            PotionHeal();
        }
        else if(other.tag == "Gold" & gameObject.tag != "Weapon")
        {
            Destroy(other.gameObject);
        }
    }
}
