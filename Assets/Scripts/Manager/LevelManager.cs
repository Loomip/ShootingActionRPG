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

    // ���� ��ȯ�� ���� ���� �����ϱ� ���� �ӽ� ����
    private int currentMonstersOnMap;

    public void StartRound(int roundNumber)
    {
        // ���忡 ���� ���̵� ����
        MonstersToSpawn = roundNumber * 16;
        MonstersDefeated = 0;
        currentMonstersOnMap = 0;

        // ���� ��ȯ �ڷ�ƾ ����
        StartCoroutine(SpawnMonsters());
    }

    // ���� ��ȯ �ڷ�ƾ
    private IEnumerator SpawnMonsters()
    {
        while (currentMonstersOnMap < MonstersToSpawn)
        {
            if (currentMonstersOnMap < maxMonstersOnMap)
            {
                // ������ ������ ����
                int spawnerIndex = Random.Range(0, spawners.Length);
                EnemySpawn spawner = spawners[spawnerIndex];

                // ������ �����ʿ��� ���� 1���� ��ȯ
                spawner.SpawnMonster();
                currentMonstersOnMap++;

                Debug.Log(" ��ȯ �Ǵ� ���� �� : " + currentMonstersOnMap);
            }

            // ��� ���Ͱ� ��ȯ�Ǿ����� �ڷ�ƾ ����
            if (currentMonstersOnMap == MonstersToSpawn)
            {
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void OnMonsterDefeated()
    {
        // ���Ͱ� �й��ϸ� ī��Ʈ ����
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
