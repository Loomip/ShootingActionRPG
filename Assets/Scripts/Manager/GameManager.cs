using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("����")]
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

    private void Start()
    {
        StartNextRound();
    }
}