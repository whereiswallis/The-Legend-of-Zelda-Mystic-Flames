using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int money;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.M)){
            money += 10000;
            UpdateMoney();
        }
        */
    }

    public void UpdateMoney(){
        txt.text = money.ToString();
    }

    public void Spend(int cost){
        money -= cost;
        UpdateMoney();
    }
}
