using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("라운드")]
    [SerializeField] private LevelManager levelManager; // 레벨 매니저 참조
    [SerializeField] private TextMeshProUGUI txt_DeathCount;
    [SerializeField] private int currentRound = 0; // 현재 라운드

    void StartNextRound()
    {
        currentRound++;
        levelManager.StartRound(currentRound); // 레벨 매니저에게 다음 라운드 시작을 알림
        UpdateUI();
    }

    public void OnLevelCleared()
    {
        // 대기 시간 후 다음 라운드 시작
        Invoke("StartNextRound", 5.0f);
    }

    // 데스 카운트 리프레쉬
    public void UpdateUI()
    {
        int remainingMonsters = levelManager.MonstersToSpawn - levelManager.MonstersDefeated;
        txt_DeathCount.text =  remainingMonsters + " / " + levelManager.MonstersToSpawn;
    }

    //=================================================================================================================

    [Header("UI")]
    [SerializeField] private Slider playerHp;
    
    // 생성된 각 몬스터의 체력바를 저장할 딕셔너리
    [SerializeField] private Dictionary<Health, Slider> enemyHealthBars = new Dictionary<Health, Slider>();

    // 몬스터와 그에 해당하는 체력바를 딕셔너리에 등록
    public void RegisterEnemyHealthBar(Health enemyHealth, Slider healthBar)
    {
        enemyHealthBars[enemyHealth] = healthBar;
    }

    // 체력 리프레쉬
    public void RefreshHp(string tag, Health entity)
    {
        switch (tag)
        {
            case "Player":
                playerHp.value = (float)entity.Hp / entity.MaxHp;
                break;
            case "Enemy":
                if (enemyHealthBars.TryGetValue(entity, out Slider enemyHp))
                {
                    enemyHp.value = (float)entity.Hp / entity.MaxHp;
                }
                break;
        }
    }

    //=================================================================================================================

    [Header("돈")]
    //골드
    [SerializeField] TextMeshProUGUI goldText;

    // 현재 골드를 저장하는 변수
    private int gold = 0;

    //골드 UI를 리프레쉬 해주는 함수
    public void Refresh_Gold()
    {
        if (goldText != null)
            goldText.text = string.Format("{0: #,##0}", gold);
    }

    public void Add_Gold(int addGold)
    {
        gold += addGold;
    }

    //=================================================================================================================

    private void Start()
    {
        Refresh_Gold();
        StartNextRound();
    }
}
