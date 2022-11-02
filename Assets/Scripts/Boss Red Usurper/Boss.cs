using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    Animator animator;

    public int maxHealth = 1000;
    public int currentHealth;
    //int attack = 10;
    public float attackRange = 3f;
    public GameObject player;
    public GameObject monster;
    public Collider body;
    public Collider mouth;


    public Slider healthBar;


    void Start()
    {
        maxHealth = 1000;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.Find("MaleCharacterPBR");
        body.enabled = false;
        mouth.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        /*
        if(Input.GetKeyDown(KeyCode.F)){
            FlameThrow();
        }

        if (currentHealth < (float)2/3 * maxHealth && !firstFlame){
            transform.LookAt(player.transform);
            firstFlame = true;
            FlameThrow();
        }

        if (currentHealth < (float)1/3 * maxHealth && !secondFlame){
            transform.LookAt(player.transform);
            FlameThrow();
            secondFlame = true;
        }
        */


        //if(Input.GetKeyDown(KeyCode.G)){
        //    animator.SetBool("isClawAttacking",true);

        //}
        
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


    public void FlameThrow()
    {
        animator.SetTrigger("flameThrow");
        GetComponentInChildren<ParticleSystem>().Play(true);
    }

    public void GetHit()
    {
        animator.SetTrigger("gethit");

    }

    void Die()
    {
        animator.SetTrigger("isDead");
        
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        GetComponentInChildren<Canvas>().enabled = false;
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            GetComponentInParent<roomControl>().enemies.Remove(monster);
            Invoke("EndGame", 5f);
        }
        else
        {
            LevelChanger.levelToLoad = 2;
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().Fade();
        }
        this.enabled = false;
        
    }

    public void EndGame(){
        GameObject.Find("Generate").GetComponent<GameStatistics>().convertion();
    }

    public void MouthAttack()
    {   
        mouth.enabled = true;
        
    }

    public void ClawAttack()
    {
       body.enabled = true;
    }

    public void StopClawAttack()
    {
        body.enabled = false;
    }

    public void StopMouthAttack()
    {
        mouth.enabled = false;
    }


}

