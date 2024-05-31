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

    public void SpawnMonster()
    {
        // 소환 범위 지정
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        // 소환 위치 랜덤 지정
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

        // 소환할 몬스터 랜덤 지정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // 지정된 몬스터 소환
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
    }
}
