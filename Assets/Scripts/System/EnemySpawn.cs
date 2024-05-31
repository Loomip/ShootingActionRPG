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
        // ��ȯ ���� ����
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        // ��ȯ ��ġ ���� ����
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

        // ��ȯ�� ���� ���� ����
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // ������ ���� ��ȯ
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
    }
}
