using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyAttackableState
{
    // 대기 상태 시작(진입) 처리 (상태 초기화)
    public override void EnterState(e_EnemyState state)
    {
        // 네비게이션 에이전트 이동 정지
        nav.isStopped = false;

        animator.SetInteger("State", (int)state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {

    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

    }
}
