using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSack : MonoBehaviour
{
    private int value = 30;
    private GameObject wallet;
    void Start()
    {
        wallet = GameObject.Find("Money");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wallet.GetComponent<Money>().money = wallet.GetComponent<Money>().money + 
                (int) (value * (1 + (0.5 * GameStatistics.goldLevel)));
            wallet.GetComponent<Money>().UpdateMoney();
            Destroy(gameObject);
        }
    }
    
}
