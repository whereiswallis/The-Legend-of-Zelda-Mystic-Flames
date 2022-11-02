using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_2 : MonoBehaviour
{
    public string ability2_name;
    public bool available;
    public Slider slider;
    private int cooldown = 60;
    private float time_passed;

    public HealthBar player_healthbar;
    public GameObject player;

    public int ability_time = 15;
    public float ability2_active_time;
    public bool ability2_active;

    // Start is called before the first frame update
    void Start()
    {
        ability2_name = "";
        available = false;
        ability2_active = false;

        player_healthbar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Character");
        if((slider.value) == 0 && (ability2_name != "")){
            available = true;
        }
        else if (ability2_name != ""){
            time_passed += Time.deltaTime;
            slider.value = (1-time_passed/cooldown);
        }
        if (ability2_active_time > ability_time && ability2_active){
            ability2_active = false;
            EndAbility2();
        }
        else if (ability2_active_time <= ability_time){
            ability2_active_time += Time.deltaTime;
        }
        
    }

    public void Activate(){
        DetermineAbility();
        ability2_active_time = 0;
        ability2_active = true;
        available = false;
        slider.value = 1;
        time_passed = 0;
    }

    public void SetName(string name){
        ability2_name = name;
    }

    public void DetermineAbility(){
        if(ability2_name == "Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack += 20;
        }
        else if(ability2_name == "Heal"){
            player = GameObject.Find("Character");
            player.GetComponentInChildren<PlayerHealth_modified>().player_health += (int) (player.GetComponentInChildren<PlayerHealth_modified>().player_max_health / 2);
            if(player.GetComponentInChildren<PlayerHealth_modified>().player_health > player.GetComponentInChildren<PlayerHealth_modified>().player_max_health){
                player.GetComponentInChildren<PlayerHealth_modified>().player_health = player.GetComponentInChildren<PlayerHealth_modified>().player_max_health;
            }
            player_healthbar.setHealth(player.GetComponentInChildren<PlayerHealth_modified>().player_health);
        }
        else if(ability2_name == "Speed"){
            GameStatistics.speedLevel += 4;
        }
        else if(ability2_name == "Defence"){
            GameStatistics.defenceLevel += 3;
        }
        else if(ability2_name == "Atk Speed"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attackRate += 0.5f;
        }
        else if(ability2_name == "Big Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack += 100;
        }
    }

    public void EndAbility2(){
        if(ability2_name == "Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack -= 20;
        }
        else if(ability2_name == "Heal"){

        }
        else if(ability2_name == "Speed"){
            GameStatistics.speedLevel -= 4;
        }
        else if(ability2_name == "Defence"){
            GameStatistics.defenceLevel -= 3;
        }
        else if(ability2_name == "Atk Speed"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attackRate -= 0.5f;
        }
        else if(ability2_name == "Big Damage"){
            GameObject.Find("Character").GetComponentInChildren<PlayerAnimationsModified>().attack -= 100;
        }
    }
}
