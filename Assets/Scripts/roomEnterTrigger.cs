using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomEnterTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            GetComponentInParent<roomControl>().PlayerEnter();
        }
    }

    // Update is called once per frame

}
