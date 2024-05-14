using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Է� �̵� ó�� ������Ʈ
public class CharacterInputMovement : MonoBehaviour
{
    [SerializeField] private VariableJoystick joy;

    // �ִϸ����� ������Ʈ
    private Animator animator;

    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    private CharacterController characterController;

    // �̵��ӵ�
    [SerializeField] private float speed;

    // �̵� ����
    private Vector3 movement;

    // ���� ���� ����
    public bool grounded = false;
    float vSpeed = 0.0f; // ���� �̵� �ӵ�

    // �߷� ��ġ
    [SerializeField] private float gravity;

    private float h; // �¿� �̵� ����
    private float v; // ���� �̵� ����

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        GravityDown();
    }

    private void Move()
    {
        // Ű�Է� ó��
        float hJoy = joy.Horizontal;
        float vJoy = joy.Vertical;
        float hKey = Input.GetAxis("Horizontal");
        float vKey = Input.GetAxis("Vertical");

        h = hJoy != 0 ? hJoy : hKey;
        v = vJoy != 0 ? vJoy : vKey;

        // �̵� ���� ���� ����
        movement = new Vector3(h, 0f, v).normalized;

        // �̵� �ִϸ��̼� ���
        animator.SetFloat("Move", movement.magnitude);

        // ĳ���� ȸ�� ó��
        transform.LookAt(transform.position + movement.normalized);

        characterController.Move(movement * (speed * Time.deltaTime));
    }

    // �߷� ��Ʈ��
    private void GravityDown()
    {
        // ���� �߷��� ������ (-10)
        vSpeed = vSpeed - gravity * Time.deltaTime;

        // �ϰ��ӵ��� -10 ���� �۾�����
        if (vSpeed < -gravity)
            vSpeed = -gravity; // �ִ� �ϰ��ӵ��� -10���� ������

        // �߷� ���� �ϰ� �ӵ��� ����� ���� �̵� ���͸� ������
        var verticalMove = new Vector3(0, vSpeed * Time.deltaTime, 0);

        // �߷°��� ����� ���� �ϰ� �̵��� ó����
        var flag = characterController.Move(verticalMove);

        // ĳ���� ��Ʈ�ѷ��� ���鿡 ��Ҵٸ�
        if ((flag & CollisionFlags.Below) != 0)
        {
            // ���� �ϰ� �ӵ��� 0���� ������
            vSpeed = 0;
        }
    }
}
