using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyAttackableState
{
    // ��� ���� ����(����) ó�� (���� �ʱ�ȭ)
    public override void EnterState(e_EnemyState state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        nav.isStopped = false;

        animator.SetInteger("State", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {

    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }
}
