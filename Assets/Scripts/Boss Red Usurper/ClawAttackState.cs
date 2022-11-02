using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttackState : StateMachineBehaviour
{   
    GameObject player;
    GameObject monster;
    float preTime =1f;
    float damageTime = 1.8f;
    float animationTime = 3f;
    float startDamage;
    float endDamage;
    float endAnimation;

    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponentInParent<Boss>().player;
        monster = animator.GetComponentInParent<Boss>().monster;
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        
        startDamage = Time.time + preTime;
        endDamage = Time.time + damageTime;
        endAnimation = Time.time + animationTime;
        animator.SetBool("switchAttack",false);
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        //animator.transform.LookAt(player.transform);
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);

        animator.SetBool("isClawAttacking",false);

        if (Time.time < startDamage)
        {
            animator.transform.LookAt(player.transform);
        }


        else if (Time.time > startDamage && Time.time < endDamage)
        {
            monster.GetComponentInParent<Boss>().ClawAttack();
        }
        else if (Time.time > endDamage && Time.time < endAnimation)
        {
           monster.GetComponentInParent<Boss>().StopClawAttack();
        } 

        else if (Time.time > endAnimation){
            startDamage = Time.time + preTime;
            endDamage = Time.time + damageTime;
            endAnimation = Time.time + animationTime;

        }

        if (distance < 12f)
        {
            animator.SetBool("switchAttack",true);
        }
        else animator.SetBool("switchAttack",false);


        


        


        
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        
        monster.GetComponentInParent<Boss>().StopClawAttack();
        
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
