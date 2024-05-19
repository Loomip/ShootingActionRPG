using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAttackableState
{
    // 공격 타겟 중심점 위치
    [SerializeField] private Transform attackTransfom;

    // 공격 범위
    [SerializeField] private float attackRadius;

    // 공격 범위 각도
    [SerializeField] private float hitAngle;

    // 공격 대상 레이어
    [SerializeField] protected LayerMask targetLayer;

    // 회전 보간 수치
    [SerializeField] protected float smoothValue;

    // 공격 대상을 주시
    protected void LookAtTarget()
    {
        // 공격 대상을 향한 방향을 계산
        Vector3 direction = (controller.Player.transform.position - transform.position).normalized;

        // 회전 쿼터니언 계산
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // 보간 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothValue);
    }

    public void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(attackTransfom.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            Vector3 directionToTargert = hit.transform.position - transform.position;

            float angleToTarget = Vector3.Angle(transform.forward, directionToTargert);

            if (angleToTarget < hitAngle)
            {
                // Player 피격 처리
                if (hit.tag == "Player")
                {
                    // 히트 판정
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
        // 죽엇으면 리턴

        // 공격 범위를 넘어가면
        if (controller.GetPlayerDistance() > attackDistance)
        {
            // 달리기 상태로 전환
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
