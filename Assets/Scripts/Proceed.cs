using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proceed : MonoBehaviour
{
    private bool isInside = false;
    private Generate Generate;


    private void Start()
    {
        Generate = GameObject.Find("Generate").GetComponent<Generate>();
        Generate.setHintOff();
        //hint = GameObject.Find("ProceedHint");
        //hint.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInside)
        {
            Generate.canProceed = true;
            isInside = false;
            Generate.setHint();
            if(GameObject.Find("boss"))
            {
                Invoke("delayDestroy", 1.5f);
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInside = true;
            Generate.setHint();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInside = false;
            Generate.setHintOff();
        }
    }

    public void setHint()
    {
        //hint.SetActive(true);
        Generate.setHint();
    }

    public void delayDestroy()
    {
        Destroy(GameObject.Find("boss"));

    }
}
