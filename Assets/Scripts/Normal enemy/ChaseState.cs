using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    float timer;

    //bool hasAttacked = false;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.speed = 5f;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        timer += Time.deltaTime;
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        agent.SetDestination(player.transform.position);


        //if (timer > 2+Time.time && hasAttacked){
        //    hasAttacked = false;
        //}


        if (distance < 5){
            agent.speed = 0;
        }
        else agent.speed =5;
        if (distance > 30)
        {
            animator.SetBool("isChasing",false);
        }
        if (distance < 5f && timer > 2)
        {
            //animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking",true);
            //animator.Play("Attack01");
            //hasAttacked = true;
            //timer = Time.time +2;
            
            
        }
        
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
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
