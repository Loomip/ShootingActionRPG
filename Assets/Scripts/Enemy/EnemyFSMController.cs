using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : MonoBehaviour
{
    // ������ ���� ���� ���� ���� ������Ʈ
    [SerializeField] private EnemyState currentState;

    // ������ ��� ���� ������Ʈ��
    [SerializeField] private EnemyState[] EnemyStatas;

    // ȸ�� ���� ��ġ
    [SerializeField] protected float smoothValue;

    // �÷��̾� ����
    protected GameObject player;
    public GameObject Player { get => player; set => player = value; }

    [SerializeField] private GameObject[] headModels; // �Ӹ� �� �迭
    [SerializeField] private GameObject[] bodyModels; // �� �� �迭
    [SerializeField] private GameObject[] WeaponModels; // ���� �� �迭

    // ��� �� ������ ������
    [SerializeField] protected GameObject[] items;
    [SerializeField] protected float[] dropChances; // ������ ��� Ȯ�� �迭

    private const float totalChance = 100f; // ��ü ��� Ȯ��

    // ������ ���� ���
    [SerializeField] private int dropGold;

    // ���� ��ȯ �޼ҵ�
    public void TransactionToState(e_EnemyState state)
    {
        currentState?.ExitState(); // ���� ���� ����
        currentState = EnemyStatas[(int)state]; // ���� ��ȯ ó��
        currentState.EnterState(state); // ���ο� ���� ����
    }

    // ���� ��Ʈ�ѷ� ��ɵ�

    // �÷��̾�� ���Ͱ��� �Ÿ� ����
    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    // ���� ����� �ֽ�
    public void LookAtTarget()
    {
        // ���� ����� ���� ������ ���
        Vector3 direction = (Player.transform.position - transform.position).normalized;

        // ȸ�� ���ʹϾ� ���
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // ���� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothValue);
    }

    // �÷��̾�� ������ ����
    public void Hit()
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (currentState == EnemyStatas[(int)e_EnemyState.Die]) return;

        // �ǰ� ���·� ��ȯ
        TransactionToState(e_EnemyState.Hit);
    }

    public void Knockdown()
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (currentState == EnemyStatas[(int)e_EnemyState.Die]) return;

        TransactionToState(e_EnemyState.Knockdown);
    }

    public void Death()
    {
        // ���� ���·� ��ȯ
        TransactionToState(e_EnemyState.Die);
    }

    public void DropItem()
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

    public void DropGold()
    {
        GameManager.instance.Add_Gold(dropGold);
        GameManager.instance.Refresh_Gold();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // ��� �Ӹ��� �� ���� ��Ȱ��ȭ
        foreach (var model in headModels) model.SetActive(false);
        foreach (var model in bodyModels) model.SetActive(false);

        if (WeaponModels != null)
        {
            foreach (var model in WeaponModels)
                model.SetActive(false);
        }

        // �Ӹ��� �� �� �߿��� �����ϰ� ����
        GameObject headModel = headModels[Random.Range(0, headModels.Length)];
        GameObject bodyModel = bodyModels[Random.Range(0, bodyModels.Length)];

        // ������ �Ӹ��� �� ���� Ȱ��ȭ
        headModel.SetActive(true);
        bodyModel.SetActive(true);

        if (WeaponModels != null && WeaponModels.Length > 0)
        {
            GameObject weaponModel = WeaponModels[Random.Range(0, WeaponModels.Length)];
            weaponModel.SetActive(true);
        }

        // ��� ���·� ����
        TransactionToState(e_EnemyState.Idle);
    }

    private void Update()
    {
        // ���� ������ ������ ����� ����
        currentState?.UpdateState();
    }
}
