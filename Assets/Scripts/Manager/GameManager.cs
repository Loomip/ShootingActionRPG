using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LevelManager levelManager; // ���� �Ŵ��� ����
    private int currentRound = 0; // ���� ����

    void StartNextRound()
    {
        currentRound++;
        levelManager.StartRound(currentRound); // ���� �Ŵ������� ���� ���� ������ �˸�
    }

    public void OnLevelCleared()
    {
        // ��� �ð� �� ���� ���� ����
        Invoke("StartNextRound", 5.0f);
    }

    private void Start()
    {
        StartNextRound();
    }
}
