using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;


    void Start(){
        slider = GetComponentInParent<Slider>();
    }

    public void SetMaxHealth(int max_health){
        slider.maxValue = max_health;
        slider.value = max_health;
    }

    // Update is called once per frame
    public void setHealth(int health){
        slider.value = health;
    }
}
