using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWakeUpState : EnemyState
{
    // ��� ���� ����(����) ó�� (���� �ʱ�ȭ)
    public override void EnterState(e_EnemyState state)
    {
        nav.isStopped = true;

        animator.SetInteger("state", (int)state);
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
