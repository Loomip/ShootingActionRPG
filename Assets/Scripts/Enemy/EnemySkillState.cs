using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyAttackableState
{
    // ���� ��� ���̾�
    [SerializeField] protected LayerMask targetLayer;

    // ��ų ������
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
        // �׾����� ����
        if (Health.Hp <= 0)
        {
            controller.Death();
            return;
        }

        // ���� ������ �Ѿ��
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // �޸��� ���·� ��ȯ
            controller.TransactionToState(e_EnemyState.Run);
            return;
        }

        controller.LookAtTarget();
    }

    public override void ExitState()
    {

    }
}
