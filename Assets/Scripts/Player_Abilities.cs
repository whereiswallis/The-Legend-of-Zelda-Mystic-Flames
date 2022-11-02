using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Abilities : MonoBehaviour
{

    public GameObject ability_1;
    public GameObject ability_2;

    // Start is called before the first frame update
    void Start()
    {
        ability_1 = GameObject.Find("Ability 1");
        ability_2 = GameObject.Find("Ability 2");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(ability_1.GetComponent<Ability_1>().available){
                ability_1.GetComponent<Ability_1>().Activate();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(ability_2.GetComponent<Ability_2>().available){
                ability_2.GetComponent<Ability_2>().Activate();
            }
        }
        
    }
}
