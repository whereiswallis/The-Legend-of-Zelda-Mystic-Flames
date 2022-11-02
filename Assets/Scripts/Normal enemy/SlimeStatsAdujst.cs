using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStatsAdujst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   

        GetComponent<Enemy>().setMaxHealth(50);
        GetComponentInChildren<AttackDamage>().setDamage(5);
        GetComponentInChildren<AttackDamage>().setBlockedDamage(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
