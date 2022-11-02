using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellAttackState : StateMachineBehaviour
{   
    GameObject player;
    GameObject monster;
    float preTime =.33f;
    float damageTime = .67f;
    float animationTime = 1f;
    float startDamage;
    float endDamage;
    float endAnimation;
    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monster = animator.GetComponentInParent<Enemy>().monster;
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        startDamage = Time.time + preTime;
        endDamage = Time.time + damageTime;
        endAnimation = Time.time + animationTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.transform.LookAt(player.transform);
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);

        if (Time.time > startDamage && Time.time < endDamage)
        {
            monster.GetComponentInParent<Enemy>().Attack();
        }
        else if (Time.time > endDamage && Time.time < endAnimation)
        {
            monster.GetComponentInParent<Enemy>().StopAttack();

        } 

        else if (Time.time > endAnimation){
            startDamage = Time.time + preTime;
            endDamage = Time.time + damageTime;
            endAnimation = Time.time + animationTime;

        }





        if (distance > 5.5f)
        {
            animator.SetBool("isAttacking",false);
        }

        
        
    }




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        
        monster.GetComponentInParent<Enemy>().StopAttack();
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
