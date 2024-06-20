using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Enemy 프리팹들
    [SerializeField] private GameObject[] enemyPrefabs;

    // Enemy 생성 위치
    [SerializeField] private Transform spawnTransform;

    // 생성 범위
    [SerializeField] private float spawnRange;

    // 각 몬스터의 소환 확률
    [SerializeField] private float[] spawnProbabilities;

    public void SpawnMonster()
    {
        // 소환 범위 지정
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        // 소환 위치 랜덤 지정
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

        // 확률 기반으로 소환할 몬스터 인덱스 결정
        int enemyIndex = GetRandomProbabilityIndex(spawnProbabilities);

        // 지정된 몬스터 소환
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
    }

    // 확률에 따른 랜덤 인덱스 반환 (총 확률은 100%)
    private int GetRandomProbabilityIndex(float[] probabilities)
    {
        float total = 0;

        // 전체 확률 합계 계산
        foreach (float probability in probabilities)
        {
            total += probability;
        }

        // 랜덤 확률 값 결정
        float randomPoint = Random.value * total;

        // 확률에 따라 인덱스 결정
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (randomPoint < probabilities[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probabilities[i];
            }
        }
        return probabilities.Length - 1;
    }
}
