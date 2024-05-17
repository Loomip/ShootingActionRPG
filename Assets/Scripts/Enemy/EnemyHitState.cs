using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackableState
{
    // �ǰ� ��ƼŬ
    [SerializeField] protected ParticleSystem hitParticle;

    public override void EnterState(e_EnemyState state)
    {
        // �̵� ����
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
