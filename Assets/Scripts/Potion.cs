using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private GameObject Character;
    void Start()
    {
        Character = GameObject.Find("MaleCharacterPBR");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Character.GetComponent<PlayerHealth_modified>().PotionHeal();
            Destroy(gameObject);
        }
    }
}
