using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyAttackableState
{
    // ���� ��� ���̾�
    [SerializeField] protected LayerMask targetLayer;

    // ��ų ������
    [SerializeField] protected GameObject enemySkill;

    // ��ų �ߵ� ��ġ
    [SerializeField] protected Transform SkillPos;

    // ���ݷ�
    [SerializeField] protected int atk;

    // ������ 
    [SerializeField] private float jumpSpeed;

    // �߷�
    [SerializeField] private float gravity;

    // ����������
    [SerializeField] protected bool isJump;

    public bool IsJump { get => isJump; set => isJump = value; }

    public void SkillAttack()
    {
        GameObject Effect = Instantiate(enemySkill, SkillPos.position, enemySkill.transform.rotation);
        EnemyEffect akt = Effect.GetComponent<EnemyEffect>();
        akt.Atk = atk;
    }

    public IEnumerator JumpCoroutine()
    {
        // ���� ���̿� ���� �ð��� �����մϴ�.
        float jumpHeight = 1.5f; // ���� ����
        float jumpDuration = 1f; // ���� ���� �ð�

        // ���� ���� ��ġ�� ���� ������Ʈ�� ��ġ�� �����մϴ�.
        Vector3 startPos = nav.transform.position;

        Vector3 newVelocity = new Vector3(0, nav.velocity.y, 0);

        // ���� ���� ��ġ�� �����մϴ�.
        Vector3 endPos = startPos + newVelocity * jumpDuration;

        // ��� �ð��� �ʱ�ȭ�մϴ�.
        float elapsedTime = 0f;

        // ������ ���ӵǴ� ���� �ݺ��մϴ�.
        while (elapsedTime < jumpDuration)
        {
            // ��� �ð��� ���� ������ ������� ����մϴ�.
            float t = elapsedTime / jumpDuration;

            // �ð��� ���� ������ ���̸� �����Ͽ� �ڿ������� ������ �����մϴ�.
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;

            // ���� ���� ���� ������Ʈ�� ��ġ�� �����մϴ�.
            nav.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;

            Anima.SetFloat("vertical", elapsedTime);

            // ��� �ð��� ������Ʈ�մϴ�.
            elapsedTime += Time.deltaTime;

            // �� �������� ��ٸ��ϴ�.
            yield return null;
        }
    }

    public override void EnterState(e_EnemyState state)
    {
        if (!IsJump)
        {
            nav.isStopped = true;

            nav.speed = 0f;

            Anima.SetInteger("state", (int)state);

            StartCoroutine("JumpCoroutine");
        }
    }

    public override void UpdateState()
    {
        if (IsJump) return;

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
        nav.isStopped = false;

        nav.speed = 1f;
    }
}
