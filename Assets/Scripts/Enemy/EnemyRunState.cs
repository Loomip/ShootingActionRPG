using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyAttackableState
{
    // ��� ���� ����(����) ó�� (���� �ʱ�ȭ)
    public override void EnterState(e_EnemyState state)
    {
        // ���� ���ǵ� ����
        nav.speed = 3.5f;

        // ���� ��� ���� ó��
        nav.isStopped = false;

        animator.SetInteger("state", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �׾����� ����
        if (Health.Hp <= 0)
        {
            controller.Death();
            return;
        }

        // �÷��̾ ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(e_EnemyState.Attack);
            return;
        }

        // ���� ��� ���� ó��
        nav.isStopped = false;
        nav.SetDestination(controller.Player.transform.position);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }
}
