using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackableState
{
    // 피격 파티클
    [SerializeField] protected ParticleSystem hitParticle;

    public override void EnterState(e_EnemyState state)
    {
        // 이동 중지
        nav.isStopped = true;

        animator.SetInteger("State", (int)state);

    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
