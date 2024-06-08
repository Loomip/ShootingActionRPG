using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyAttackableState
{
    // 공격 대상 레이어
    [SerializeField] protected LayerMask targetLayer;

    // 스킬 프리펩
    [SerializeField] protected GameObject enemySkill;

    public void SkillAttack()
    {

    }

    public override void EnterState(e_EnemyState state)
    {
        nav.isStopped = true;

        nav.speed = 0f;

        animator.SetInteger("state", (int)state);
    }

    public override void UpdateState()
    {
        // 죽엇으면 리턴
        if (Health.Hp <= 0)
        {
            controller.Death();
            return;
        }

        // 공격 범위를 넘어가면
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // 달리기 상태로 전환
            controller.TransactionToState(e_EnemyState.Run);
            return;
        }

        controller.LookAtTarget();
    }

    public override void ExitState()
    {

    }
}
