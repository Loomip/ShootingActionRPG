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

    public void SpawnMonsters(int numMonsters)
    {
        for (int i = 0; i < numMonsters; i++)
        {
            // 랜덤한 생성 위치 설정
            float x = Random.Range(-spawnRange, spawnRange);
            float z = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

            // 균등한 확률로 몬스터를 생성
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);

            // 확률에 따른 몬스터 생성
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
        }
    }
}
