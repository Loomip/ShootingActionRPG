using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackableState
{
    // 피격 파티클
    [SerializeField] protected ParticleSystem hitParticle;

    private bool isHit = false;

    public bool IsHit { get => isHit; set => isHit = value; }

    public override void EnterState(e_EnemyState state)
    {
        isHit = true;

        // 이동 중지
        nav.isStopped = true;

        // 히트 이펙트 실행
        //hitParticle.Play();

        // 히트 모션 실행
        animator.SetInteger("state", (int)state);
    }

    public override void UpdateState()
    {
        if(!IsHit)
        {
            // 플레이어가 공격 가능 거리안에 들어왔다면
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // 공격 상태로 전환
                controller.TransactionToState(e_EnemyState.Attack);
                return;
            }

            // 공격 범위를 넘어가면
            if (controller.GetPlayerDistance() > attackDistance)
            {
                // 달리기 상태로 전환
                controller.TransactionToState(e_EnemyState.Run);
                return;
            }
        }
    }

    public override void ExitState()
    {

    }
}
