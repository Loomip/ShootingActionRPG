using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMController : MonoBehaviour
{
    // 몬스터의 현재 동작 중인 상태 컴포넌트
    [SerializeField] private EnemyState currentState;

    // 몬스터의 모든 상태 컴포넌트들
    [SerializeField] private EnemyState[] EnemyStatas;

    // 플레이어 참조
    protected GameObject player;
    public GameObject Player { get => player; set => player = value; }

    [SerializeField] private GameObject[] headModels; // 머리 모델 배열
    [SerializeField] private GameObject[] bodyModels; // 몸 모델 배열
    [SerializeField] private GameObject[] WeaponModels; // 무기 모델 배열


    // 상태 전환 메소드
    public void TransactionToState(e_EnemyState state)
    {
        currentState?.ExitState(); // 이전 상태 정리
        currentState = EnemyStatas[(int)state]; // 상태 전환 처리
        currentState.EnterState(state); // 세로운 상태 전이
    }

    // 보조 컨트롤러 기능들

    // 플레이어와 몬스터간의 거리 측정
    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    // 플레이어에게 공격을 받음
    public void Hit()
    {
        // 현재 상태가 이미 사망한 상태면 피격 처리하지 않음
        if (currentState == EnemyStatas[(int)e_EnemyState.Die]) return;

        // 피격 상태로 전환
        TransactionToState(e_EnemyState.Hit);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // 모든 머리와 몸 모델을 비활성화
        foreach (var model in headModels) model.SetActive(false);
        foreach (var model in bodyModels) model.SetActive(false);

        if (WeaponModels != null)
        {
            foreach (var model in WeaponModels)
                model.SetActive(false);
        }

        // 머리와 몸 모델 중에서 랜덤하게 선택
        GameObject headModel = headModels[Random.Range(0, headModels.Length)];
        GameObject bodyModel = bodyModels[Random.Range(0, bodyModels.Length)];

        // 선택한 머리와 몸 모델을 활성화
        headModel.SetActive(true);
        bodyModel.SetActive(true);

        if (WeaponModels != null && WeaponModels.Length > 0)
        {
            GameObject weaponModel = WeaponModels[Random.Range(0, WeaponModels.Length)];
            weaponModel.SetActive(true);
        }

        // 대기 상태로 시작
        TransactionToState(e_EnemyState.Idle);
    }

    private void Update()
    {
        // 현재 설정된 상태의 기능을 동작
        currentState?.UpdateState();
    }
}
