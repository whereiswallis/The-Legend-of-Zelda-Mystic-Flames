using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameThrow : MonoBehaviour
{
    public ParticleSystem part;
    int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void OnParticleCollision(GameObject other) 
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerAnimationsModified>().isDefend())
            {
                other.GetComponent<PlayerHealth_modified>().TakeDefendDamage(damage);
            }
            else 
                other.GetComponent<PlayerHealth_modified>().TakeDamage(damage);
        }

   }
}
