using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyAttackableState
{
    // �ǰ� ��ƼŬ
    [SerializeField] protected ParticleSystem hitParticle;

    private bool isHit = false;

    public bool IsHit { get => isHit; set => isHit = value; }

    public override void EnterState(e_EnemyState state)
    {
        isHit = true;

        // �̵� ����
        nav.isStopped = true;

        // ��Ʈ ����Ʈ ����
        //hitParticle.Play();

        // ��Ʈ ��� ����
        animator.SetInteger("state", (int)state);
    }

    public override void UpdateState()
    {
        if(!IsHit)
        {
            // �÷��̾ ���� ���� �Ÿ��ȿ� ���Դٸ�
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // ���� ���·� ��ȯ
                controller.TransactionToState(e_EnemyState.Attack);
                return;
            }

            // ���� ������ �Ѿ��
            if (controller.GetPlayerDistance() > attackDistance)
            {
                // �޸��� ���·� ��ȯ
                controller.TransactionToState(e_EnemyState.Run);
                return;
            }
        }
    }

    public override void ExitState()
    {

    }
}
