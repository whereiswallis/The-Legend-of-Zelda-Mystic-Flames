using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyGolemStatsAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Enemy>().setMaxHealth(50);
        GetComponentInChildren<AttackDamage>().setDamage(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
