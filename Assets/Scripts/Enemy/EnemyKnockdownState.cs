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

    // ��� �� ������ ������
    [SerializeField] protected GameObject[] items;
    [SerializeField] protected float[] dropChances; // ������ ��� Ȯ�� �迭

    private const float totalChance = 100f; // ��ü ��� Ȯ��

    [SerializeField] private bool isKnockDown = false;

    public bool IsKnockDown { get => isKnockDown; set => isKnockDown = value; }

    void DropItem()
    {
        float remainingChance = totalChance; // ���� Ȯ���� ��ü Ȯ���� �ʱ�ȭ

        // ������ �迭�� ���̿� ��� Ȯ�� �迭�� ���̰� ������ Ȯ��
        if (items.Length != dropChances.Length)
        {
            Debug.LogError("�����۰� ��� Ȯ���� ������ ��ġ���� �ʽ��ϴ�.");
            return;
        }

        // ��� �����ۿ� ���� Ȯ�� ����
        for (int i = 0; i < items.Length; i++)
        {
            // �������� ��� Ȯ���� 0���� �۰ų� ���� ��� ������ ó��
            if (dropChances[i] <= 0)
            {
                Debug.LogWarning(items[i].name + "�� ��� Ȯ���� 0���� �۰ų� �����ϴ�. �� �������� ��ӵ��� �ʽ��ϴ�.");
                continue;
            }

            // �������� ��� Ȯ���� ���� Ȯ������ Ŭ ��� ���� Ȯ���� �������� ��� Ȯ���� ����
            if (dropChances[i] > remainingChance)
            {
                dropChances[i] = remainingChance;
            }

            // ���� Ȯ������ �������� ��� Ȯ���� ����, �̸� �ٽ� ���� Ȯ���� ������Ʈ
            remainingChance -= dropChances[i];
        }

        // �������� �������� ����
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            cumulativeChance += dropChances[i];
            if (randomValue <= cumulativeChance)
            {
                // ���õ� �������� ���
                Instantiate(items[i], transform.position + items[i].transform.position, items[i].transform.rotation);
                return;
            }
        }
    }

    public override void EnterState(e_EnemyState state)
    {
        isKnockDown = true;

        animator.SetInteger("state", (int)state);
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
                DropItem();
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
