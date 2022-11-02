using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthBar : MonoBehaviour
{

    public Transform cam;
    void Start(){
        cam = GameObject.Find("Main Camera").transform;
    }
    // Update is called once per frame
    void Update()
    {
        //if (cam)
            cam = GameObject.Find("Main Camera").transform;
        transform.LookAt(cam);
        
    }
}
