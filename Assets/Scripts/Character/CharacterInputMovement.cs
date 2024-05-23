using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Է� �̵� ó�� ������Ʈ
public class CharacterInputMovement : MonoBehaviour
{
    // �̵� ���̽�ƽ ������Ʈ
    //[SerializeField] private VariableJoystick moveJoy;

    // �ִϸ����� ������Ʈ
    private Animator animator;

    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    private CharacterController characterController;

    // �̵��ӵ�
    [SerializeField] private float speed;

    // �̵� ����
    private Vector3 movement;

    // ĳ���� ȸ���� ���� ���콺 ����Ʈ ����ĳ��Ʈ �ٴ� �浹 ���̾� ����ũ
    [SerializeField] private LayerMask floorMask;

    // ĳ���� ȸ���� ���� ���콺 ����Ʈ ����ĳ��Ʈ ����
    [SerializeField] private float camRayLength;

    // �߷� ��ġ
    [SerializeField] private float gravity;

    private float h; // �¿� �̵� ����
    private float v; // ���� �̵� ����

    private PHealth health;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        health = GetComponent<PHealth>();
    }

    private void Update()
    {
        Move();
        Turning();
    }

    private void Move()
    {
        if (health.IsDie) return;

        // Ű�Է� ó��
        //float hJoy = moveJoy.Horizontal;
        //float vJoy = moveJoy.Vertical;
        //float hKey = Input.GetAxis("Horizontal");
        //float vKey = Input.GetAxis("Vertical");

        //h = hJoy != 0 ? hJoy : hKey;
        //v = vJoy != 0 ? vJoy : vKey;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // �̵� ���� ���� ����
        movement = new Vector3(h, 0f, v).normalized;

        // �̵� �ִϸ��̼� ���
        animator.SetFloat("Move", movement.magnitude);

        // �߷� ����
        movement.y -= gravity * Time.deltaTime;

        // �̵� ó��
        characterController.Move(movement * (speed * Time.deltaTime));
    }


    // ĳ���� ȸ�� ó�� (���콺 ������ �������� ĳ���Ͱ� �ٶ󺸰�)
    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouseDirection = floorHit.point - transform.position;
            playerToMouseDirection.y = 0f;

            Quaternion rotation = Quaternion.LookRotation(playerToMouseDirection);

            // ĳ���� ȸ�� ó��
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }
}