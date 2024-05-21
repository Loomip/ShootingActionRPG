using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LevelManager levelManager; // 레벨 매니저 참조
    private int currentRound = 0; // 현재 라운드

    void StartNextRound()
    {
        currentRound++;
        levelManager.StartRound(currentRound); // 레벨 매니저에게 다음 라운드 시작을 알림
    }

    public void OnLevelCleared()
    {
        // 대기 시간 후 다음 라운드 시작
        Invoke("StartNextRound", 5.0f);
    }

    private void Start()
    {
        StartNextRound();
    }
}
