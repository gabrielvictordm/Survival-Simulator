using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Combate : StateMachineBehaviour
{
    private GameObject Player = null;
    private GameController GameControllerObject = null;
    private NavMeshAgent NavMesh;
    private bool flag_liberar_vida = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player");
        NavMesh = animator.transform.GetComponent<NavMeshAgent>();
        GameControllerObject = GameObject.Find("GameController").GetComponent<GameController>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player != null)
        {
            float distancia = Vector3.Distance(Player.transform.position, animator.transform.position);
            animator.SetFloat("distancia", distancia);
        }
        else
        {
            animator.SetFloat("distancia", 20);
        }

        if (Player != null)
        {
            NavMesh.destination = Player.transform.position;
        }

        if ((stateInfo.normalizedTime % 1) < .2f && flag_liberar_vida == true)
        {
            flag_liberar_vida = false;
        }

        if ((stateInfo.normalizedTime % 1) > .75f && flag_liberar_vida == false)
        {
            flag_liberar_vida = true;
            
            if (GameControllerObject)
            {
                GameControllerObject.DescontarVida();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     NavMesh.speed = BackupVelocidade;
    // }

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
