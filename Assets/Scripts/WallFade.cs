using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    // Start is called before the first frame update

    Shader normalShader;
    Shader fadeShader;

    void Start()
    {
        normalShader = Shader.Find("Standard");
        fadeShader = Shader.Find("Unlit/FadeShader");
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider other )
    {
        if (other.tag == "Player")
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.shader = fadeShader;
            }
        }
    }



    void OnTriggerExit (Collider other )
    {
        if (other.tag == "Player")
        {
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.shader = normalShader;
            }
        }
    }

}
