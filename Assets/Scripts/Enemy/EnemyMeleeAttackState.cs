using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackableState
{
    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransfom;

    // ���� ����
    [SerializeField] private float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // ���� ��� ���̾�
    [SerializeField] protected LayerMask targetLayer;

    // ���ݷ�
    [SerializeField] protected int atk;

    public void MeleeAttack()
    {
        // ������ ���۵Ǿ����� ����� �α׷� ���

        // ���� ���� ���� �浹ü Ž��
        Collider[] hits = Physics.OverlapSphere(attackTransfom.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 directionToTarget = hit.transform.position - transform.position;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget < hitAngle)
            {
                // �÷��̾� �ǰ� ó��
                if (hit.CompareTag("Player"))
                {
                    // ��Ʈ ����
                    PHealth pHealth = hit.GetComponent<PHealth>();
                    if (pHealth != null)
                    {
                        pHealth.Hit(atk);
                    }
                }
            }
        }
    }

    public override void EnterState(e_EnemyState state)
    {
        nav.isStopped = true;

        nav.speed = 0f;

        Anima.SetInteger("state", (int)state);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransfom.position, attackRadius);
    }
}
