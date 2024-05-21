using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemySpawn[] spawners; // ������ �迭 ����
    private int monstersToSpawn; // ��ȯ�� ���� ��

    public void StartRound(int roundNumber)
    {
        // ���忡 ���� ���̵� ����
        monstersToSpawn = roundNumber * 16;
        int monstersPerSpawner = monstersToSpawn / spawners.Length; // �� �����ʴ� ��ȯ�� ���� ��

        foreach (EnemySpawn spawner in spawners)
        {
            spawner.SpawnMonsters(monstersPerSpawner); // �� �����ʿ��� ��ȯ�� ���� ���� �˸�
        }
    }

    public void OnMonsterDefeated()
    {
        // ���Ͱ� �й��ϸ� ī��Ʈ ����
        monstersToSpawn--;
        if (monstersToSpawn <= 0)
        {
            // ��� ���Ͱ� �й��ϸ� GameManager���� ���� Ŭ��� �˸�
            GameManager.instance.OnLevelCleared();
        }
    }
}
