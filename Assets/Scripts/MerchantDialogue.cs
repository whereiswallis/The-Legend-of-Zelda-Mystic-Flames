using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MerchantDialogue : MonoBehaviour
{

    public GameObject upgradesMenu;
    public bool leavingMerchant = false;
    public float time = 0f;
    public Transform player;
    public Transform merchant;
    public float distance;
    public static float activationDistance = 100f;
    public Text dialogue;

    List<string> dialogueOptions = new List<string>() {
        "Yahoo!! I'm Mr. Meeseeks. How can I help you?", 
        "Wouldn't it be crazy if we're just part of a school CompSci project? ... Sorry I mean how can I help?", 
        "My mum said I wasn't anything but an NPC - a Nearly Perfect Child. Anyway how can I help?",
        "I'm burning through the sky, yeah, two hundred degrees, that's why they call me Mister Fahrenheit... ahghm sorry how can I help?",
        "What do you mean why is there a dancing monster wearing a party hat and selling upgrades? Do you want something or not?",
        "... So as you can see there really is evidence to suggest that information can travel faster than the speed of light. Sorry I was just finishing my paper, how can I help?",
        "These upgrades really are to DIE for. So what would you like?",
        "What would you say is the airspeed of an unladen swallow? Have a think while you browse",
        "This really is a massive upgrade from Saphirre Seas! My graphics feel so smooth! Anyway what would you like to buy?",
        "... So I said 'No Shrek 2 is definitely the greatest piece of modern film' although I have to say Surfs Up does give it a run for its money. What would you like?"
        };

    // Start is called before the first frame update
    void Start()
    {
        List<string> dialogueOptions = new List<string>() {
        "Yahoo!! I'm Mr. Meeseeks. How can I help you?", 
        "Wouldn't it be crazy if we're just part of a school CompSci project? ... Sorry I mean how can I help?", 
        "My mum said I wasn't anything but an NPC - a Nearly Perfect Child. Anyway how can I help?",
        "I'm burning through the sky, yeah, two hundred degrees, that's why they call me Mister Fahrenheit... ahghm sorry how can I help?",
        "What do you mean why is there a dancing monster wearing a party hat and selling upgrades? Do you want something or not?",
        "... So as you can see there really is evidence to suggest that information can travel faster than the speed of light. Sorry I was just finishing my paper, how can I help?",
        "These upgrades really are to DIE for. So what would you like?",
        "What would you say is the airspeed of an unladen swallow? Have a think while you browse",
        "This really is a massive upgrade from Saphirre Seas! My graphics feel so smooth! Anyway what would you like to buy?",
        "... So I said 'No Shrek 2 is definitely the greatest piece of modern film' although I have to say Surfs Up does give it a run for its money. What would you like?"
        };
        //upgradesMenu = GameObject.Find("Upgrade Menu");
        //upgradesMenu.SetActive(false);
        //player = GameObject.Find("Character3").transform;
        //merchant = GameObject.Find("P02").transform;
        
    }

    // Update is called once per frame
    void Update()
    {   
        

        if(leavingMerchant) {
            time += Time.deltaTime;
            if(time > 1.5){
                gameObject.SetActive(false);
                time = 0;
                leavingMerchant = false;
            }
        }
    }

    public void showDialogue(){
        if (!gameObject.activeSelf) {
            System.Random r = new System.Random();
            int rInt = r.Next(0, 10);
            dialogue.text = dialogueOptions[rInt];
        }
        gameObject.SetActive(true);
        leavingMerchant = false;
        time = 0;
        
    }

    public void GenerateDialogue(){
        System.Random r = new System.Random();
        int rInt = r.Next(0, 10);
        dialogue.text = dialogueOptions[rInt];
    }

    public void EnterShop(){
        gameObject.SetActive(false);
        upgradesMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void EndDialogue(){
        leavingMerchant = true;
        time = 0;
    }

    
}
