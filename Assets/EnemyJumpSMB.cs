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
        // 애니메이션이 거의 끝나면 대기 코루틴을 시작합니다.
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

        // 점프가 종료되었음을 나타내는 플래그를 해제합니다.
        animator.GetComponent<EnemySkillState>().IsJump = false;
    }


    IEnumerator Wait(Animator animator, float waitTime)
    {
        // 대기 상태로 설정합니다.
        animator.GetComponent<EnemySkillState>().IsWaiting = true;

        yield return new WaitForSeconds(waitTime);

        // 대기 상태를 해제합니다.
        animator.GetComponent<EnemySkillState>().IsWaiting = false;
    }
}
