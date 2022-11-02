using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocked : MonoBehaviour
{

    Shader seeThruShader;
    Shader normalShader;
    GameObject player;
    public float timer;
    //private bool hasChanged = false;
    private bool ignoreNext = false;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MaleCharacterPBR");
        seeThruShader = Shader.Find("Unlit/AlwaysVisible");
        normalShader = Shader.Find("Standard");
        
    }


    void Update()
    {

        player = GameObject.Find("MaleCharacterPBR");
        //if (Time.time > timer && !ignoreNext){
            //foreach (Renderer rend in player.GetComponentsInChildren<Renderer>())
            //{
            //    rend.material.shader = normalShader;
            //}
            //hasChanged = false;
            
        //} 
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Walls")
        {
            foreach (Renderer rend in player.GetComponentsInChildren<Renderer>())
            {
                rend.material.shader = seeThruShader;
            }
            //hasChanged = false;
            ignoreNext = true;
            count++;
            
           
        }
        
    }

    
    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Walls")
        {
            if (!ignoreNext || count == 1)
            {
                foreach (Renderer rend in player.GetComponentsInChildren<Renderer>())
                {
                    rend.material.shader = normalShader;
                }
                count = 0;
            }
            else
            {
                ignoreNext = false;
            } 
        }
        
        
    }
    
}
