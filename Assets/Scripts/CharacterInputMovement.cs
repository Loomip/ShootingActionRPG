using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 입력 이동 처리 컴포넌트
public class CharacterInputMovement : MonoBehaviour
{
    [SerializeField] private VariableJoystick joy;

    // 애니메이터 컴포넌트
    private Animator animator;

    // 캐릭터 컨트롤러 컴포넌트
    private CharacterController characterController;

    // 이동속도
    [SerializeField] private float speed;

    // 이동 벡터
    private Vector3 movement;

    // 지면 착지 여부
    public bool grounded = false;
    float vSpeed = 0.0f; // 수직 이동 속도

    // 중력 수치
    [SerializeField] private float gravity;

    private float h; // 좌우 이동 방향
    private float v; // 상하 이동 방향

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
        // 키입력 처리
        float hJoy = joy.Horizontal;
        float vJoy = joy.Vertical;
        float hKey = Input.GetAxis("Horizontal");
        float vKey = Input.GetAxis("Vertical");

        h = hJoy != 0 ? hJoy : hKey;
        v = vJoy != 0 ? vJoy : vKey;

        // 이동 방향 벡터 설정
        movement = new Vector3(h, 0f, v).normalized;

        // 이동 애니메이션 재생
        animator.SetFloat("Move", movement.magnitude);

        // 캐릭터 회전 처리
        transform.LookAt(transform.position + movement.normalized);

        characterController.Move(movement * (speed * Time.deltaTime));
    }

    // 중력 컨트롤
    private void GravityDown()
    {
        // 수직 중력을 적용함 (-10)
        vSpeed = vSpeed - gravity * Time.deltaTime;

        // 하강속도가 -10 보다 작아지면
        if (vSpeed < -gravity)
            vSpeed = -gravity; // 최대 하강속도를 -10으로 설정함

        // 중력 수직 하강 속도가 적용된 수직 이동 벡터를 설정함
        var verticalMove = new Vector3(0, vSpeed * Time.deltaTime, 0);

        // 중력값이 적용된 수직 하강 이동을 처리함
        var flag = characterController.Move(verticalMove);

        // 캐릭터 컨트롤러가 지면에 닿았다면
        if ((flag & CollisionFlags.Below) != 0)
        {
            // 수직 하강 속도를 0으로 설정함
            vSpeed = 0;
        }
    }
}
