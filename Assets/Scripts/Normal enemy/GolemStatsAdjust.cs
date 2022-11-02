using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStatsAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Enemy>().setMaxHealth(200);
        GetComponentInChildren<AttackDamage>().setDamage(30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
