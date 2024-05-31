using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // ���� �ʿ� ��ȯ�� ���� ���� �����ϱ� ���� �ӽ� ����
    private int currentMonstersOnMap;

    // ���� ���忡 ��ȯ�� �� ���� ���� �����ϱ� ���� ����
    private int totalSpawnedMonsters;

    public void StartRound(int roundNumber)
    {
        // ���忡 ���� ���̵� ����
        MonstersToSpawn = roundNumber * 16;

        // ��� ���� �ʱ�ȭ
        MonstersDefeated = 0;
        currentMonstersOnMap = 0;
        totalSpawnedMonsters = 0;

        // ���� ��ȯ �ڷ�ƾ ����
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (totalSpawnedMonsters < MonstersToSpawn)
        {
            if (currentMonstersOnMap < maxMonstersOnMap)
            {
                // ������ ������ ����
                int spawnerIndex = Random.Range(0, spawners.Length);
                EnemySpawn spawner = spawners[spawnerIndex];

                // ������ �����ʿ��� ���� ��ȯ
                spawner.SpawnMonster();
                currentMonstersOnMap++;
                totalSpawnedMonsters++;

                Debug.Log("��ȯ�Ǵ� ���� ��: " + currentMonstersOnMap);

                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Debug.Log("���� �ʿ� �ִ� ���� ���� �ִ�ġ�� �����߽��ϴ�. ��� ��...");
                yield return new WaitForSeconds(1f); // 1�� �Ŀ� �ٽ� ����
            }
        }

        Debug.Log("��� ���Ͱ� ��ȯ�Ǿ����ϴ�.");
    }

    public void OnMonsterDefeated()
    {
        // ���Ͱ� �й��ϸ� ī��Ʈ ����
        currentMonstersOnMap--;
        MonstersDefeated++;
        GameManager.instance.UpdateUI();

        Debug.Log(" ���� ���� �� : " + MonstersDefeated);


        // ��� ���Ͱ� �й��ϸ� 
        if (MonstersDefeated >= MonstersToSpawn)
        {
            // GameManager���� ���� Ŭ��� �˸�
            GameManager.instance.OnLevelCleared();
        }
    }
}
