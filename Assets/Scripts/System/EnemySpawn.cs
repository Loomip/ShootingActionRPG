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

    public void SpawnMonsters(int numMonsters)
    {
        for (int i = 0; i < numMonsters; i++)
        {
            // ������ ���� ��ġ ����
            float x = Random.Range(-spawnRange, spawnRange);
            float z = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

            // �յ��� Ȯ���� ���͸� ����
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);

            // Ȯ���� ���� ���� ����
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
        }
    }
}
