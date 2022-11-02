using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    public GameObject player;
    int damage  = 20;
    int blockedDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MaleCharacterPBR");
    }

    void Update()
    {
        player = GameObject.Find("MaleCharacterPBR");
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player.GetComponent<PlayerAnimationsModified>().isDefend())
            {
                player.GetComponent<PlayerHealth_modified>().TakeDefendDamage(blockedDamage);
            }
            else 
            player.GetComponent<PlayerHealth_modified>().TakeDamage(damage);
        }

    }


    public void setDamage(int damage)
    {
        this.damage = damage;
    }


    public void setBlockedDamage(int blockedDamage)
    {
        this.blockedDamage = blockedDamage;
    }
}
