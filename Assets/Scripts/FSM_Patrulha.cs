using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Patrulha : StateMachineBehaviour
{
    public string NomeAreaWaypoints;
    private GameObject Player = null;
    private GameObject AreaWaypoint = null;
    private Transform[] Waypoints;
    private int WaypointEacolhido = 0;
    private NavMeshAgent NavMesh;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player");
        NavMesh = animator.transform.GetComponent<NavMeshAgent>();
        AreaWaypoint = GameObject.Find(NomeAreaWaypoints);

        if (AreaWaypoint)
        {
            Waypoints = new Transform[AreaWaypoint.transform.childCount];

            for (int i = 0; i < Waypoints.Length; i++)
            {
                Waypoints[i] = AreaWaypoint.transform.GetChild(i).transform;
            }
        }

        if (Waypoints.Length > 0)
        {
            WaypointEacolhido = Random.Range(0, Waypoints.Length);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player != null)
        {
            float distancia = Vector3.Distance(Player.transform.position, animator.transform.position);
            animator.SetFloat("distancia", distancia);
        }else{
            animator.SetFloat("distancia", 20);
        }

        if(Vector3.Distance(Waypoints[WaypointEacolhido].transform.position, animator.transform.position) < 2.5f){
            if(Waypoints.Length > 0) {
                WaypointEacolhido = Random.Range(0, Waypoints.Length);
            } 
        }

        NavMesh.destination = Waypoints[WaypointEacolhido].transform.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
