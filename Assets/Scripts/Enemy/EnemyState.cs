using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    // �ִϸ����� ����
    protected Animator animator;

    // �׺� ������Ʈ ����
    protected NavMeshAgent nav;

    // ���� ���ѻ��±�� ��Ʈ�ѷ�
    protected EnemyFSMController controller;

    // ���� �Ŵ��� ������Ʈ
    protected LevelManager levelManager;

    protected Health health;

    protected Collider collider;

    // ���� ���� ���� �������̽�(�����ƴ�) �޼ҵ� ����

    // ���� ���� ���� (�ٸ����·� ���̵�) �޼ҵ�
    public abstract void EnterState(e_EnemyState state);

    // ���� ���� ������Ʈ �߻� �޼ҵ� (���� ���� ����)
    public abstract void UpdateState();

    // ���� ���� ���� (�ٸ����·� ���̵�) �޼ҵ�
    public abstract void ExitState();

    private void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        controller = GetComponent<EnemyFSMController>();
        health = GetComponent<Health>();
        levelManager = FindObjectOfType<LevelManager>();
        collider = GetComponent<Collider>();
    }
}
