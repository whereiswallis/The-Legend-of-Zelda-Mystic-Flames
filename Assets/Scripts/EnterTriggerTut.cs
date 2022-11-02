using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTriggerTut : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            GetComponentInParent<roomControlTut>().PlayerEnter();
        }
    }
}
