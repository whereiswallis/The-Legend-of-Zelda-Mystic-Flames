using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolemStatsAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Enemy>().setMaxHealth(100);
        GetComponentInChildren<AttackDamage>().setDamage(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
