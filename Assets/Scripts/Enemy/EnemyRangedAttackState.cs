using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackableState
{
    // ���� ��� ���̾�
    [SerializeField] protected LayerMask targetLayer;

    // �Ѿ� ������
    [SerializeField] protected GameObject enemyBullet;

    // �Ѿ� �߻� ��ġ
    [SerializeField] protected Transform bulletPos;

    // ���ݷ�
    [SerializeField] protected int atk;

    public void RangedAttack()
    {
        // ��� ã��
        Collider[] targets = Physics.OverlapSphere(transform.position, 11f, targetLayer);

        if (targets.Length > 0)
        {
            // ���� ���� ã�� ��� ����
            Transform target = targets[0].transform;

            // ����� ������ ����
            if (target == null) return;

            GameObject proj = Instantiate(this.enemyBullet, bulletPos.position, this.enemyBullet.transform.rotation);
            EnemyBullet enemyBullet = proj.GetComponent<EnemyBullet>();
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            enemyBullet.Atk = atk;
            rb.velocity = enemyBullet.CalculateLaunchData(target, bulletPos).initialVelocity;

        }
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
