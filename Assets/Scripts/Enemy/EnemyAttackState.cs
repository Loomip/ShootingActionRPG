using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAttackableState
{
    // ���� Ÿ�� �߽��� ��ġ
    [SerializeField] private Transform attackTransfom;

    // ���� ����
    [SerializeField] private float attackRadius;

    // ���� ���� ����
    [SerializeField] private float hitAngle;

    // ���� ��� ���̾�
    [SerializeField] protected LayerMask targetLayer;

    // ȸ�� ���� ��ġ
    [SerializeField] protected float smoothValue;

    [SerializeField] protected int atk;

    // ���� ����� �ֽ�
    protected void LookAtTarget()
    {
        // ���� ����� ���� ������ ���
        Vector3 direction = (controller.Player.transform.position - transform.position).normalized;

        // ȸ�� ���ʹϾ� ���
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // ���� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothValue);
    }

    public void Attack()
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

        animator.SetInteger("state", (int)state);
    }

    public override void UpdateState()
    {
        // �׾����� ����
        if (health.Hp <= 0)
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

        LookAtTarget();
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
