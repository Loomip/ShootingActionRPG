using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemySpawn[] spawners; // 스포너 배열 참조
    private int monstersToSpawn; // 소환할 몬스터 수

    public void StartRound(int roundNumber)
    {
        // 라운드에 따라 난이도 조절
        monstersToSpawn = roundNumber * 16;
        int monstersPerSpawner = monstersToSpawn / spawners.Length; // 각 스포너당 소환할 몬스터 수

        foreach (EnemySpawn spawner in spawners)
        {
            spawner.SpawnMonsters(monstersPerSpawner); // 각 스포너에게 소환할 몬스터 수를 알림
        }
    }

    public void OnMonsterDefeated()
    {
        // 몬스터가 패배하면 카운트 감소
        monstersToSpawn--;
        if (monstersToSpawn <= 0)
        {
            // 모든 몬스터가 패배하면 GameManager에게 레벨 클리어를 알림
            GameManager.instance.OnLevelCleared();
        }
    }
}
