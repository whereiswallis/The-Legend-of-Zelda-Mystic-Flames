using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{
    [SerializeField]GameObject player;
    int damage  = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Boss>().player;
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<PlayerAnimationsModified>().isDefend())
        {
            player.GetComponent<PlayerHealth_modified>().TakeDefendDamage(damage);
            Debug.Log("25 damage taken");
        }
        else 
        {
            player.GetComponent<PlayerHealth_modified>().TakeDamage(damage);
            Debug.Log("50 damage taken");

        }

    }
}
