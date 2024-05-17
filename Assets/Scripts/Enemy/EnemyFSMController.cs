using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : MonoBehaviour
{
    // ������ ���� ���� ���� ���� ������Ʈ
    [SerializeField] private EnemyState currentState;

    // ������ ��� ���� ������Ʈ��
    [SerializeField] private EnemyState[] monsterStatas;

    // �÷��̾� ����
    protected GameObject player;
    public GameObject Player { get => player; set => player = value; }

    // ���� ��ȯ �޼ҵ�
    public void TransactionToState(e_EnemyState state)
    {
        currentState?.ExitState(); // ���� ���� ����
        currentState = monsterStatas[(int)state]; // ���� ��ȯ ó��
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
        if (currentState == monsterStatas[(int)e_EnemyState.Die]) return;

        // �ǰ� ���·� ��ȯ
        TransactionToState(e_EnemyState.Hit);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // ��� ���·� ����
        TransactionToState(e_EnemyState.Idle);
    }

    private void Update()
    {
        // ���� ������ ������ ����� ����
        currentState?.UpdateState();
    }
}
