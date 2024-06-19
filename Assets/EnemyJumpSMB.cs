using UnityEngine;
using System.Collections;

public class EnemyJumpSMB : StateMachineBehaviour
{
    private bool isWaiting = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isWaiting = false;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ִϸ��̼��� ���� ������ ��� �ڷ�ƾ�� �����մϴ�.
        if (stateInfo.normalizedTime >= 0.9f && !isWaiting)
        {
            isWaiting = true;
            animator.GetComponent<EnemySkillState>().StartCoroutine(Wait(animator, 5f));
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyState>().Anima.SetFloat("vertical", 0f);

        // ������ ����Ǿ����� ��Ÿ���� �÷��׸� �����մϴ�.
        animator.GetComponent<EnemySkillState>().IsJump = false;
    }


    IEnumerator Wait(Animator animator, float waitTime)
    {
        // ��� ���·� �����մϴ�.
        animator.GetComponent<EnemySkillState>().IsWaiting = true;

        yield return new WaitForSeconds(waitTime);

        // ��� ���¸� �����մϴ�.
        animator.GetComponent<EnemySkillState>().IsWaiting = false;
    }
}
