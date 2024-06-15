using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockdownState : EnemyAttackableState
{
    // ��� �Ϸ� ó�� �ð�
    protected float time;
    [SerializeField] protected float deathDelayTime;

    // ��� ó�� ����Ʈ
    [SerializeField] protected GameObject destroyParticlePrefab;

    private bool isKnockDown = false;

    public bool IsKnockDown { get => isKnockDown; set => isKnockDown = value; }

    public override void EnterState(e_EnemyState state)
    {
        isKnockDown = true;

        Anima.SetInteger("state", (int)state);
    }

    public override void UpdateState()
    {
        // �˹� ���̸� ����
        if (IsKnockDown) return;

        // �˹� ������ ������
        if (Health.Hp <= 0)
        {
            // ��� ó�� �����ð� ����
            time += Time.deltaTime;

            //�ݶ��̴��� ����
            col.isTrigger = true;

            // ��� ó�� �����ð��� �����ٸ�
            if (time >= deathDelayTime)
            {
                // ��� �̴��� �˷���
                levelManager.OnMonsterDefeated();
                // ��� ����Ʈ ����
                Instantiate(destroyParticlePrefab, transform.position, destroyParticlePrefab.transform.rotation);
                // ���� ������ ����
                controller.DropItem();
                // ��� ��������
                controller.DropGold();
                // ��ü �ı�
                Destroy(gameObject);
            }
        }

        else if(Health.Hp > 0 && !IsKnockDown)
        {
            controller.TransactionToState(e_EnemyState.WakeUp);
        }
    }

    public override void ExitState()
    {
        
    }
}
