using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //private bool hasChild;
    float disable = 1f;
    float Timer;

    void Start()
    {
        //if(gameObject.transform.childCount > 0)
        //{
        //    hasChild = true;
        //}
    }

    void Update()
    {
        //if(gameObject.transform.position.y < -3)
        //{
        //    Destroy(gameObject);
        //}
        //if(hasChild == true & gameObject.transform.childCount == 0)
        //{
        //    Destroy(gameObject);
        //}
        //Destroy(gameObject, 2.0f);
        Timer += Time.deltaTime;
        if(Timer >= disable)
        {
            Destroy(gameObject);
        }
    }
}
