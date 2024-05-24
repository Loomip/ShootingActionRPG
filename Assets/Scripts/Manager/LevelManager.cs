using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 스포너 배열 참조
    [SerializeField] private EnemySpawn[] spawners;

    // 소환할 몬스터 수
    private int monstersToSpawn;
    public int MonstersToSpawn { get => monstersToSpawn; set => monstersToSpawn = value; }

    // 잡은 몬스터 수
    private int monstersDefeated;
    public int MonstersDefeated { get => monstersDefeated; set => monstersDefeated = value; }

    // 맵에 동시에 존재할 수 있는 몬스터의 최대 수
    [SerializeField] private int maxMonstersOnMap;

    public void StartRound(int roundNumber)
    {
        // 라운드에 따라 난이도 조절
        MonstersToSpawn = roundNumber * 16;
        MonstersDefeated = 0;

        // 각 스포너의 SpawnedMonsters 변수 초기화
        foreach (EnemySpawn spawner in spawners)
        {
            spawner.ResetSpawnedMonsters();
        }

        // 몬스터 소환 코루틴 시작
        StartCoroutine(SpawnMonsters());
    }

    // 몬스터 소환 코루틴
    private IEnumerator SpawnMonsters()
    {
        while (true) // 무한 루프로 변경
        {
            int currentMonstersOnMap = 0;

            foreach (EnemySpawn spawner in spawners)
            {
                currentMonstersOnMap += spawner.SpawnedMonsters;
            }

            if (currentMonstersOnMap < maxMonstersOnMap)
            {
                // 랜덤한 스포너 선택
                int spawnerIndex = Random.Range(0, spawners.Length);
                EnemySpawn spawner = spawners[spawnerIndex];

                // 선택한 스포너에서 몬스터 1마리 소환
                spawner.SpawnMonster();
                currentMonstersOnMap++;
            }

            Debug.Log("몬스터 소환 수 :" + currentMonstersOnMap);
            Debug.Log("소환 최대 몬스터 수 :" + MonstersToSpawn);

            // 모든 몬스터가 소환되었으면 코루틴 종료
            if (currentMonstersOnMap == MonstersToSpawn)
            {
                break;
            }

            yield return new WaitForSeconds(1f); // 0.5초 대기
        }
    }

    public void OnMonsterDefeated()
    {
        // 몬스터가 패배하면 카운트 증가
        MonstersDefeated++;
        GameManager.instance.UpdateUI();

        // 모든 몬스터가 패배하면 
        if (MonstersDefeated >= MonstersToSpawn)
        {
            // GameManager에게 레벨 클리어를 알림
            GameManager.instance.OnLevelCleared();
        }
    }
}
