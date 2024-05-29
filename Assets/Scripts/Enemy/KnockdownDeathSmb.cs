using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockdownDeathSmb : StateMachineBehaviour
{
    [SerializeField] private float nuckBackSize;

    float time;

    Vector3 backward;
    Quaternion rotation;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyKnockdownState>().IsKnockDown = true;
        backward = -animator.transform.forward;

        animator.GetComponent<NavMeshAgent>().updateRotation = false;
        animator.applyRootMotion = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;

        if (time > 0.5f)
        {
            animator.GetComponent<NavMeshAgent>().isStopped = true;
            animator.GetComponent<EnemyKnockdownState>().IsKnockDown = false;
        }
        else
        {
            //animator.GetComponent<NavMeshAgent>().velocity = backward * nuckBackSize;
            animator.GetComponent<NavMeshAgent>().velocity = backward * nuckBackSize;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().enabled = false;
    }
}
