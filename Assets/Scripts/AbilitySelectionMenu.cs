using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectionMenu : MonoBehaviour
{   
    public GameObject AbilitySelection;
    public GameObject PickAbility;
    public GameObject ability1;
    public GameObject ability2;
    public GameObject chest_ability1;
    public GameObject chest_ability2;
    public GameObject chest_ability3;
    public GameObject ability1_script;
    public GameObject ability2_script;

    public GameObject ability1_chest_name;
    public GameObject ability2_chest_name;
    public GameObject ability3_chest_name;

    public Sprite damage_chest_icon;
    public Sprite heal_chest_icon;
    public Sprite speed_chest_icon;
    public Sprite defence_chest_icon;
    public Sprite atkSpeed_chest_icon;
    public Sprite bigDam_chest_icon;

    public List<Sprite> chest_ability_list;
    public List<string> chest_ability_names = new List<string>() {"Damage", "Heal", "Speed", "Defence", "Atk Speed", "Big Damage"};
    public List<int> chest_images;
    public int image_added;

    public Sprite replacement_sprite;


    void Start()
    {
        chest_ability_names = new List<string>() {"Damage", "Heal", "Speed", "Defence", "Atk Speed", "Big Damage"};
        chest_ability_list = new List<Sprite>() {damage_chest_icon, heal_chest_icon, speed_chest_icon, defence_chest_icon, atkSpeed_chest_icon, bigDam_chest_icon};
        AbilitySelection = GameObject.Find("AbilitySelection");
        PickAbility = GameObject.Find("Pick Ability");
        ability1_script = GameObject.Find("Ability 1");
        ability2_script = GameObject.Find("Ability 2");
        ability1 = GameObject.Find("Ability 1").transform.GetChild(0).gameObject;
        ability2 = GameObject.Find("Ability 2").transform.GetChild(0).gameObject;
        AbilitySelection.SetActive(false);
        PickAbility.SetActive(false);
        chest_ability1 = AbilitySelection.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        chest_ability2 = AbilitySelection.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        chest_ability3 = AbilitySelection.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;

        ability1_chest_name = AbilitySelection.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        ability2_chest_name = AbilitySelection.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        ability3_chest_name = AbilitySelection.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
    }

    public void Continue()
    {
        AbilitySelection.SetActive(false);
    }

    public void SelectAbility1()
    {
        image_added = chest_images[0];
        replacement_sprite = chest_ability1.GetComponent<Image>().sprite;
        if(!chest_ability_list.Contains(ability1.GetComponent<Image>().sprite)){
            ReplaceAbility1();
        }
        else if(!chest_ability_list.Contains(ability2.GetComponent<Image>().sprite)){
            ReplaceAbility2();
        }
        else {
            ChooseAbilityReplace();
        }
        AbilitySelection.SetActive(false);
    }

    public void SelectAbility2()
    {
        image_added = chest_images[1];
        replacement_sprite = chest_ability2.GetComponent<Image>().sprite;
        if(!chest_ability_list.Contains(ability1.GetComponent<Image>().sprite)){
            ReplaceAbility1();
        }
        else if(!chest_ability_list.Contains(ability2.GetComponent<Image>().sprite)){
            ReplaceAbility2();
        }
        else {
            ChooseAbilityReplace();
        }
        AbilitySelection.SetActive(false);
    }

    public void SelectAbility3()
    {
        image_added = chest_images[2];
        replacement_sprite = chest_ability3.GetComponent<Image>().sprite;
        if(!chest_ability_list.Contains(ability1.GetComponent<Image>().sprite)){
            ReplaceAbility1();
        }
        else if(!chest_ability_list.Contains(ability2.GetComponent<Image>().sprite)){
            ReplaceAbility2();
        }
        else {
            ChooseAbilityReplace();
        }
        AbilitySelection.SetActive(false);
    }

    public void ReplaceAbility1(){
        ability1.GetComponent<Image>().sprite = replacement_sprite;
        ability1_script.GetComponent<Ability_1>().SetName(chest_ability_names[image_added]);
        PickAbility.SetActive(false);
    }

    public void ReplaceAbility2(){
        ability2.GetComponent<Image>().sprite = replacement_sprite;
        string name = chest_ability_names[image_added];
        ability2_script.GetComponent<Ability_2>().SetName(name);
        PickAbility.SetActive(false);
    }

    public void ChooseAbilityReplace(){
        AbilitySelection.SetActive(false);
        PickAbility.SetActive(true);
    }

    public void GenerateChest()
    {
        chest_images = new List<int>() {};
        System.Random r = new System.Random();
        int rInt = r.Next(0, 6);
        chest_images.Add(rInt);
        chest_ability1.GetComponent<Image>().sprite = chest_ability_list[rInt];
        ability1_chest_name.GetComponent<TextMeshProUGUI>().text = chest_ability_names[rInt];
        rInt = r.Next(0, 6);
        chest_images.Add(rInt);
        chest_ability2.GetComponent<Image>().sprite = chest_ability_list[rInt];
        ability2_chest_name.GetComponent<TextMeshProUGUI>().text = chest_ability_names[rInt];
        rInt = r.Next(0, 6);
        chest_images.Add(rInt);
        chest_ability3.GetComponent<Image>().sprite = chest_ability_list[rInt];
        ability3_chest_name.GetComponent<TextMeshProUGUI>().text = chest_ability_names[rInt];
    }
}
