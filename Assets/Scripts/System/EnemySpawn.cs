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

    // �� ������ ��ȯ Ȯ��
    [SerializeField] private float[] spawnProbabilities;

    public void SpawnMonster()
    {
        // ��ȯ ���� ����
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        // ��ȯ ��ġ ���� ����
        Vector3 spawnPosition = new Vector3(spawnTransform.position.x + x, 0f, spawnTransform.position.z + z);

        // Ȯ�� ������� ��ȯ�� ���� �ε��� ����
        int enemyIndex = GetRandomProbabilityIndex(spawnProbabilities);

        // ������ ���� ��ȯ
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, spawnTransform.rotation);
    }

    // Ȯ���� ���� ���� �ε��� ��ȯ (�� Ȯ���� 100%)
    private int GetRandomProbabilityIndex(float[] probabilities)
    {
        float total = 0;

        // ��ü Ȯ�� �հ� ���
        foreach (float probability in probabilities)
        {
            total += probability;
        }

        // ���� Ȯ�� �� ����
        float randomPoint = Random.value * total;

        // Ȯ���� ���� �ε��� ����
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
