using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText : MonoBehaviour
{
    public GameObject Guide;
    public GameObject W;
    public GameObject S;
    public GameObject A;
    public GameObject D;
    public GameObject Attack;
    public GameObject Defend;
    public GameObject Evade;
    
    
    public bool checkW = false;
    public bool checkS = false;
    public bool checkA = false;
    public bool checkD = false;

    public bool checkAttack = false;
    public bool checkDefend = false;
    public bool checkEvade = false;



    void Start()
    {
        Guide.SetActive(true);
        W.SetActive(true);
        S.SetActive(false);
        A.SetActive(false);
        D.SetActive(false);
        Attack.SetActive(false);
        Defend.SetActive(false);
        Evade.SetActive(false);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            checkW = true;
        }
        else if(Input.GetKeyDown(KeyCode.S) & checkW)
        {
            checkS = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) & checkS)
        {
            checkA = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) & checkA)
        {
            checkD = true;
        }


        else if(Input.GetMouseButton(0) & checkD)
        {
            checkAttack = true;
        }
         else if(Input.GetMouseButton(1) & checkAttack)
        {
            checkDefend = true;
        }
         else if(Input.GetKeyDown(KeyCode.Space) & checkDefend)
        {
            checkEvade = true;
        }




        if(checkW)
        {
            W.SetActive(false);
            S.SetActive(true);
        }

        if(checkS)
        {
            S.SetActive(false);
            A.SetActive(true);
        }

        if(checkA)
        {
            A.SetActive(false);
            D.SetActive(true);
        }

        if(checkD)
        {
            D.SetActive(false);
            Attack.SetActive(true);
        }

        if(checkAttack)
        {
            Attack.SetActive(false);
            Defend.SetActive(true);
        }

        if(checkDefend)
        {
            Defend.SetActive(false);
            Evade.SetActive(true);
        }



        if(checkEvade)
        {
            Guide.SetActive(false);
        }
    }
}
