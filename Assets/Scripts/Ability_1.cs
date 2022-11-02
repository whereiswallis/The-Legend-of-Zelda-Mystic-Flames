using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_1 : MonoBehaviour
{
    public bool available;
    public Slider slider;
    private int cooldown = 60;
    private float time_passed;
    public string ability1_name;

    public HealthBar player_healthbar;
    public GameObject player;
    

    public int ability_time = 15;
    public float ability1_active_time;
    public bool ability1_active;

    // Start is called before the first frame update
    void Start()
    {
        ability1_name = "";
        available = false;
        ability1_active = false;

        player_healthbar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Character");
        if((slider.value == 0) && (ability1_name != "")){
            available = true;
        }
        else if (ability1_name != ""){
            ability1_active_time += Time.deltaTime;
            time_passed += Time.deltaTime;
            slider.value = (1-time_passed/cooldown);
        }
        if (ability1_active_time > ability_time && ability1_active){
            ability1_active = false;
            EndAbility1();
        }
        else if (ability1_active_time <= ability_time){
            ability1_active_time += Time.deltaTime;
        }
    }

    public void Activate(){
        DetermineAbility();
        available = false;
        ability1_active = true;
        slider.value = 1;
        ability1_active_time = 0;
        time_passed = 0;
    }

    public void SetName(string name){
        ability1_name = name;
    }

    public void DetermineAbility(){
        if(ability1_name == "Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack += 20;
        }
        else if(ability1_name == "Heal"){
            player = GameObject.Find("Character");
            player.GetComponentInChildren<PlayerHealth_modified>().player_health += (int) (player.GetComponentInChildren<PlayerHealth_modified>().player_max_health / 2);
            if(player.GetComponentInChildren<PlayerHealth_modified>().player_health > player.GetComponentInChildren<PlayerHealth_modified>().player_max_health){
                player.GetComponentInChildren<PlayerHealth_modified>().player_health = player.GetComponentInChildren<PlayerHealth_modified>().player_max_health;
            }
            player_healthbar.setHealth(player.GetComponentInChildren<PlayerHealth_modified>().player_health);
        }
        else if(ability1_name == "Speed"){
            GameStatistics.speedLevel += 4;
        }
        else if(ability1_name == "Defence"){
            GameStatistics.defenceLevel += 3;
        }
        else if(ability1_name == "Atk Speed"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attackRate += 0.5f;
        }
        else if(ability1_name == "Big Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack += 100;
        }
    }

    public void EndAbility1(){
        if(ability1_name == "Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack -= 20;
        }
        else if(ability1_name == "Heal"){

        }
        else if(ability1_name == "Speed"){
            GameStatistics.speedLevel -= 4;
        }
        else if(ability1_name == "Defence"){
            GameStatistics.defenceLevel -= 3;
        }
        else if(ability1_name == "Atk Speed"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attackRate -= 0.5f;
        }
        else if(ability1_name == "Big Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack -= 100;
        }
    }


}
