using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : MonoBehaviour
{
    // ������ ���� ���� ���� ���� ������Ʈ
    [SerializeField] private EnemyState currentState;

    // ������ ��� ���� ������Ʈ��
    [SerializeField] private EnemyState[] EnemyStatas;

    // �÷��̾� ����
    protected GameObject player;
    public GameObject Player { get => player; set => player = value; }

    [SerializeField] private GameObject[] headModels; // �Ӹ� �� �迭
    [SerializeField] private GameObject[] bodyModels; // �� �� �迭
    [SerializeField] private GameObject[] WeaponModels; // ���� �� �迭


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

    // �÷��̾�� ������ ����
    public void Hit()
    {
        // ���� ���°� �̹� ����� ���¸� �ǰ� ó������ ����
        if (currentState == EnemyStatas[(int)e_EnemyState.Die]) return;

        // �ǰ� ���·� ��ȯ
        TransactionToState(e_EnemyState.Hit);
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
