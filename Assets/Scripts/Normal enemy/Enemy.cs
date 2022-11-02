using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public Slider healthBar;
    //public GameObject canvas;
    public int maxHealth = 100;
    int currentHealth;
    

    //int attack = 10;


    public GameObject player;

    public Transform attackPoint;

    public GameObject weapon;

    public GameObject monster;

    public int goldDrop;



    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        GetComponentInChildren<SphereCollider>().enabled = false;
        player = GameObject.Find("MaleCharacterPBR");


    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
            healthBar.value = 100 * ((float)currentHealth/ (float) maxHealth);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetHit();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void GetHit()
    {
        animator.SetTrigger("gethit");
        Debug.Log("enemy is hit");

    }

    void Die()
    {
        animator.SetBool("isdead",true);
        animator.SetTrigger("isDead");
        healthBar.value = 100 * ((float)currentHealth/ (float) maxHealth);
        GetComponentInChildren<BoxCollider>().enabled = false;
        weapon.GetComponentInChildren<Collider>().enabled = false;
        weapon.GetComponent<AttackDamage>().enabled = false;
        if (GetComponentInParent<roomControl>() != null)
            GetComponentInParent<roomControl>().enemies.Remove(monster);
        if (GetComponentInParent<roomControlTut>() != null)
            GetComponentInParent<roomControlTut>().enemies.Remove(monster);
        GetComponentInChildren<Canvas>().enabled = false;
        GameObject.Find("Money").GetComponent<Money>().money += (int) (goldDrop * 
            (1 + (0.5f * GameStatistics.goldLevel)));
        GameObject.Find("Money").GetComponent<Money>().UpdateMoney();
        if(GetComponent<GolemBoss>())
        {
            GetComponent<GolemBoss>().splitTiny();
            Destroy(gameObject);
        }

        if(GetComponent<destroyTiny>())
        {
            GetComponent<destroyTiny>().destroy();
        }

        roomControl.numGolem--;
        this.enabled = false;
        
    }

    public void Attack()
    {   
        GetComponentInChildren<SphereCollider>().enabled = true;
    }

    public void StopAttack()
    {
        GetComponentInChildren<SphereCollider>().enabled = false;
    }



    public void setMaxHealth(int health)
    {
        this.maxHealth = health;
        this.currentHealth = health;
    }
}

