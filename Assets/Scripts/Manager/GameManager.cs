using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("����")]
    [SerializeField] private LevelManager levelManager; // ���� �Ŵ��� ����
    [SerializeField] private TextMeshProUGUI txt_DeathCount;
    [SerializeField] private int currentRound = 0; // ���� ����

    void StartNextRound()
    {
        currentRound++;
        levelManager.StartRound(currentRound); // ���� �Ŵ������� ���� ���� ������ �˸�
        UpdateUI();
    }

    public void OnLevelCleared()
    {
        // ��� �ð� �� ���� ���� ����
        Invoke("StartNextRound", 5.0f);
    }

    // ���� ī��Ʈ ��������
    public void UpdateUI()
    {
        int remainingMonsters = levelManager.MonstersToSpawn - levelManager.MonstersDefeated;
        txt_DeathCount.text =  remainingMonsters + " / " + levelManager.MonstersToSpawn;
    }

    //=================================================================================================================

    [Header("UI")]
    [SerializeField] private Slider playerHp;
    
    // ������ �� ������ ü�¹ٸ� ������ ��ųʸ�
    [SerializeField] private Dictionary<Health, Slider> enemyHealthBars = new Dictionary<Health, Slider>();

    // ���Ϳ� �׿� �ش��ϴ� ü�¹ٸ� ��ųʸ��� ���
    public void RegisterEnemyHealthBar(Health enemyHealth, Slider healthBar)
    {
        enemyHealthBars[enemyHealth] = healthBar;
    }

    // ü�� ��������
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

    [Header("��")]
    //���
    [SerializeField] TextMeshProUGUI goldText;

    // ���� ��带 �����ϴ� ����
    private int gold = 0;

    //��� UI�� �������� ���ִ� �Լ�
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
