using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantTrigger : MonoBehaviour
{
    public GameObject merchantDialogue;


    void Start() 
    {
        if (GameObject.Find("Merchant Dialogue"))
            merchantDialogue = GameObject.Find("Merchant Dialogue");
        
        if (GameObject.Find("Generate"))
            GameObject.Find("Generate").GetComponent<Generate>().setMerchantOff();
        //merchantDialogue.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            merchantDialogue.GetComponent<MerchantDialogue>().GenerateDialogue();
            GameObject.Find("Generate").GetComponent<Generate>().setMerchant();
            //merchantDialogue.GetComponent<MerchantDialogue>().showDialogue();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            GameObject.Find("Generate").GetComponent<Generate>().setMerchantOff();
            //merchantDialogue.GetComponent<MerchantDialogue>().EndDialogue();
        }
    }

    public void setMerchant()
    {
        merchantDialogue.SetActive(true);
    }
}
