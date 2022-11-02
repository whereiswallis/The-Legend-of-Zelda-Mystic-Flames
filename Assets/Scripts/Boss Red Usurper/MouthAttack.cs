using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthAttack : MonoBehaviour
{
    [SerializeField]GameObject player;
    int damage  = 20;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MaleCharacterPBR");
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player.GetComponent<PlayerAnimationsModified>().isDefend())
            {
                player.GetComponent<PlayerHealth_modified>().TakeDefendDamage(damage);
                Debug.Log("10 damage taken");

            }
            else 
            {
                player.GetComponent<PlayerHealth_modified>().TakeDamage(damage);
                Debug.Log("20 damage taken");

            }
        }

    }
}
