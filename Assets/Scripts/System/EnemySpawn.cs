using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Enemy �����յ�
    [SerializeField] private GameObject[] enemyPrefabs;

    // Enemy ���� ��ġ
    [SerializeField] private Transform spawnTransform;

    // ���� ����
    [SerializeField] private float spawnRange;

    public void SpawnMonster()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
    }
}
