using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // ������ �迭 ����
    [SerializeField] private EnemySpawn[] spawners;

    // ��ȯ�� ���� ��
    private int monstersToSpawn;
    public int MonstersToSpawn { get => monstersToSpawn; set => monstersToSpawn = value; }

    // ���� ���� ��
    private int monstersDefeated;
    public int MonstersDefeated { get => monstersDefeated; set => monstersDefeated = value; }

    // �ʿ� ���ÿ� ������ �� �ִ� ������ �ִ� ��
    [SerializeField] private int maxMonstersOnMap;

    public void StartRound(int roundNumber)
    {
        // ���忡 ���� ���̵� ����
        MonstersToSpawn = roundNumber * 16;
        MonstersDefeated = 0;

        // �� �������� SpawnedMonsters ���� �ʱ�ȭ
        foreach (EnemySpawn spawner in spawners)
        {
            spawner.ResetSpawnedMonsters();
        }

        // ���� ��ȯ �ڷ�ƾ ����
        StartCoroutine(SpawnMonsters());
    }

    // ���� ��ȯ �ڷ�ƾ
    private IEnumerator SpawnMonsters()
    {
        while (true) // ���� ������ ����
        {
            int currentMonstersOnMap = 0;

            foreach (EnemySpawn spawner in spawners)
            {
                currentMonstersOnMap += spawner.SpawnedMonsters;
            }

            if (currentMonstersOnMap < maxMonstersOnMap)
            {
                // ������ ������ ����
                int spawnerIndex = Random.Range(0, spawners.Length);
                EnemySpawn spawner = spawners[spawnerIndex];

                // ������ �����ʿ��� ���� 1���� ��ȯ
                spawner.SpawnMonster();
                currentMonstersOnMap++;
            }

            Debug.Log("���� ��ȯ �� :" + currentMonstersOnMap);
            Debug.Log("��ȯ �ִ� ���� �� :" + MonstersToSpawn);

            // ��� ���Ͱ� ��ȯ�Ǿ����� �ڷ�ƾ ����
            if (currentMonstersOnMap == MonstersToSpawn)
            {
                break;
            }

            yield return new WaitForSeconds(1f); // 0.5�� ���
        }
    }

    public void OnMonsterDefeated()
    {
        // ���Ͱ� �й��ϸ� ī��Ʈ ����
        MonstersDefeated++;
        GameManager.instance.UpdateUI();

        // ��� ���Ͱ� �й��ϸ� 
        if (MonstersDefeated >= MonstersToSpawn)
        {
            // GameManager���� ���� Ŭ��� �˸�
            GameManager.instance.OnLevelCleared();
        }
    }
}
