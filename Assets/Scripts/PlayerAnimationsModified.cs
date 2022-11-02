using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsModified : MonoBehaviour
{
    private Animator animator;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    public LayerMask crateLayers;
    
    public Transform attackPoint;
    private bool isDefending = false;

    public int attack = 15;
    float nextAttackTime = 0f;
    public float attackRate = 1f; // the number indicates the time between each attack
    public bool isEvading = false;
    float evadetTimer = 0;
    float evadeTime = .3f;

    
    float evadeCD = 1f;
    float evadeCDTimer = 0;

    private bool isAttacking = false;
    float attackFlinchTimer = 0;
    float attackFlinchTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attack = 15 + (5*GameStatistics.attackLevel);
    }
        

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (Time.time >= nextAttackTime){
        
            if(Input.GetMouseButton(0) && !isDefending && !isEvading)
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
                attackFlinchTimer = Time.time + attackFlinchTime;
                isAttacking = true;
            }
        }

        if (isAttacking)
        {
            GetComponentInParent<ThirdPersonMovement>().enabled = false;
            GetComponent<Player_Rotation>().enabled = false;
            if (Time.time > attackFlinchTimer)
            {
                isAttacking = false;
                GetComponentInParent<ThirdPersonMovement>().enabled = true;
                GetComponent<Player_Rotation>().enabled = true;
            }
        }

        if (Input.GetMouseButton(1) && !isEvading)
        {
            Defend();
            isDefending = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopDefend();
            isDefending = false;
        }
        

        updateAnimations(horizontal, vertical);
        
    }

    void updateAnimations(float horizontal, float vertical) {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction != Vector3.zero){
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
        
        if(Input.GetKey(KeyCode.Space) && ! isAttacking && ! isEvading && Time.time > evadeCDTimer){
            isEvading = true;
            GetComponentInParent<ThirdPersonMovement>().speed = GetComponentInParent<ThirdPersonMovement>().speed*3;
            animator.SetTrigger("Jump");
            evadetTimer =  Time.time + evadeTime;
            evadeCDTimer = Time.time + evadeCD;
            GetComponentInParent<Player_Rotation>().enabled = false;
            if (Input.GetKey(KeyCode.D))
                transform.LookAt(new Vector3(transform.position.x+1,transform.position.y,transform.position.z));
            if (Input.GetKey(KeyCode.A))
                transform.LookAt(new Vector3(transform.position.x-1,transform.position.y,transform.position.z));
            if (Input.GetKey(KeyCode.S))
                transform.LookAt(new Vector3(transform.position.x,transform.position.y,transform.position.z-1));
            if (Input.GetKey(KeyCode.W))
                transform.LookAt(new Vector3(transform.position.x,transform.position.y,transform.position.z+1));
        }
        else if (isEvading){
            if (Time.time > evadetTimer){
                isEvading  = false;
                GetComponentInParent<ThirdPersonMovement>().speed = GetComponentInParent<ThirdPersonMovement>().speed/3;
                GetComponentInParent<Player_Rotation>().enabled = true;
            }
        }
        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            if (enemy.GetComponentInParent<Enemy>()!= null)
            {
                enemy.GetComponentInParent<Enemy>().TakeDamage(this.attack);
            }

            else if (enemy.GetComponentInParent<Boss>()!= null)
            {
                enemy.GetComponentInParent<Boss>().TakeDamage(this.attack);
            }
        }
        Collider[] hitCrates = Physics.OverlapSphere(attackPoint.position, attackRange, crateLayers);
        foreach (Collider crate in hitCrates)
        {
            crate.GetComponent<Destructible>().hitCrate();
        }

        if(GameObject.Find("Ability 1").GetComponent<Ability_1>().ability1_name == "Big Damage" && 
            GameObject.Find("Ability 1").GetComponent<Ability_1>().ability1_active){
            GameObject.Find("Ability 1").GetComponent<Ability_1>().ability1_active = false;
            this.attack -= 100;
        }
        if(GameObject.Find("Ability 2").GetComponent<Ability_2>().ability2_name == "Big Damage" && 
            GameObject.Find("Ability 2").GetComponent<Ability_2>().ability2_active){
            GameObject.Find("Ability 2").GetComponent<Ability_2>().ability2_active = false;
            this.attack -= 100;
        }
    }

    void Defend()
    {
        animator.SetBool("isDefend",true);
        GetComponentInParent<ThirdPersonMovement>().enabled = false;
        //GetComponentInParent<Player_Rotation>().enabled = false;

    }

    void StopDefend()
    {
        animator.SetBool("isDefend",false);
        GetComponentInParent<ThirdPersonMovement>().enabled = true;
        //GetComponentInParent<Player_Rotation>().enabled = true;
    }

    public bool isDefend()
    {
        return isDefending;
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
}
