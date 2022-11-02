using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStateBoss : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent = animator.GetComponent<NavMeshAgent>();
       player = GameObject.FindGameObjectWithTag("Player");
       agent.speed = 3f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position,player.transform.position);
        

        if (distance < 12f)
        {
            agent.speed = 0;
        }
        else agent.speed = 3;

        if (distance < 15f)
        {
            animator.SetBool("isAttacking", true);
        }
        else if ((distance > 17f && distance < 20f ))
        {
            animator.SetBool("isClawAttacking", true);
            
        }
        else if (distance > 25f)
        {
            animator.GetComponentInParent<Boss>().FlameThrow();
        }

        agent.SetDestination(player.transform.position);

        
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
