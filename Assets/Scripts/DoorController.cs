using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator[] myDoor = new Animator[2];
    //[SerializeField] private bool openTrigger = false;
    //[SerializeField] private bool closeTrigger = false;
    
    private bool isClosed = true;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if (isClosed){
                for (int i = 0; i<myDoor.Length; i++){
                myDoor[i].Play("open",0,0f);
                }
                isClosed = false;
            }
            else if (!isClosed){
                for (int i = 0; i<myDoor.Length; i++){
                myDoor[i].Play("close",0,0f);
                }
                isClosed = true;
            }
        }

    }


    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            if (isClosed){
                for (int i = 0; i<myDoor.Length; i++){
                myDoor[i].Play("open",0,0f);
                }
                isClosed = false;
            }
            else if (!isClosed){
                for (int i = 0; i<myDoor.Length; i++){
                myDoor[i].Play("close",0,0f);
                }
                isClosed = true;
            }
        }

    }

}
