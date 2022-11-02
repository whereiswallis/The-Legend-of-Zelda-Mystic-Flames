using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merchantTriggerTut : MonoBehaviour
{
    public GameObject merchantDialogue;


    void Start() 
    {
        if (GameObject.Find("MerchantDialogue"))
            merchantDialogue = GameObject.Find("Merchant Dialogue");
        merchantDialogue.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            merchantDialogue.GetComponent<MerchantDialogue>().showDialogue();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            merchantDialogue.GetComponent<MerchantDialogue>().EndDialogue();
        }
    }

    public void setMerchant()
    {
        merchantDialogue.SetActive(true);
    }
}
