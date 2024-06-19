using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyAttackableState
{
    // ��� ���� ����(����) ó�� (���� �ʱ�ȭ)
    public override void EnterState(e_EnemyState state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        //nav.isStopped = true;

        // ��� �ִϸ��̼� ���
        //animator.SetInteger("state", (int)state);

        if (controller.GetPlayerDistance() < attackDistance)
        {
            controller.TransactionToState(e_EnemyState.Attack);
            return;
        }
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �׾����� ����

        // �׾����� ����
        if (Health.Hp <= 0)
        {
            controller.Death();
            return;
        }

        // �÷��̾� �ν�
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(e_EnemyState.Run);
            return;
        }

    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }
}
