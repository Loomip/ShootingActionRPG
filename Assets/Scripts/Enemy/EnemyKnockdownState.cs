using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockdownState : EnemyAttackableState
{
    // 사망 완료 처리 시간
    protected float time;
    [SerializeField] protected float deathDelayTime;

    // 사망 처리 이펙트
    [SerializeField] protected GameObject destroyParticlePrefab;

    // 사망 후 생성될 아이템
    [SerializeField] protected GameObject[] items;
    [SerializeField] protected float[] dropChances; // 아이템 드롭 확률 배열

    private const float totalChance = 100f; // 전체 드롭 확률

    [SerializeField] private bool isKnockDown = false;

    public bool IsKnockDown { get => isKnockDown; set => isKnockDown = value; }

    void DropItem()
    {
        float remainingChance = totalChance; // 남은 확률을 전체 확률로 초기화

        // 아이템 배열의 길이와 드롭 확률 배열의 길이가 같은지 확인
        if (items.Length != dropChances.Length)
        {
            Debug.LogError("아이템과 드롭 확률의 개수가 일치하지 않습니다.");
            return;
        }

        // 모든 아이템에 대해 확률 조정
        for (int i = 0; i < items.Length; i++)
        {
            // 아이템의 드롭 확률이 0보다 작거나 같은 경우 꽝으로 처리
            if (dropChances[i] <= 0)
            {
                Debug.LogWarning(items[i].name + "의 드롭 확률이 0보다 작거나 같습니다. 이 아이템은 드롭되지 않습니다.");
                continue;
            }

            // 아이템의 드롭 확률이 남은 확률보다 클 경우 남은 확률을 아이템의 드롭 확률로 조정
            if (dropChances[i] > remainingChance)
            {
                dropChances[i] = remainingChance;
            }

            // 남은 확률에서 아이템의 드롭 확률을 빼고, 이를 다시 남은 확률로 업데이트
            remainingChance -= dropChances[i];
        }

        // 아이템을 랜덤으로 선택
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        for (int i = 0; i < items.Length; i++)
        {
            cumulativeChance += dropChances[i];
            if (randomValue <= cumulativeChance)
            {
                // 선택된 아이템을 드롭
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
        // 넉백 중이면 리턴
        if (IsKnockDown) return;

        // 넉백 됫을때 죽으면
        if (Health.Hp <= 0)
        {
            // 사망 처리 지연시간 시작
            time += Time.deltaTime;

            //콜라이더를 꺼줌
            col.isTrigger = true;

            // 사망 처리 지연시간이 지났다면
            if (time >= deathDelayTime)
            {
                // 사망 됫는지 알려줌
                levelManager.OnMonsterDefeated();
                // 사망 이팩트 생성
                Instantiate(destroyParticlePrefab, transform.position, destroyParticlePrefab.transform.rotation);
                // 랜덤 아이템 생성
                DropItem();
                // 시체 파괴
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
