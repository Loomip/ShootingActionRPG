using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EHeath : Health
{
    // ���� ü�¹�
    [SerializeField] private GameObject healthBarPrefab;

    protected IEnumerator IsHitCoroutine(int damage)
    {
        CanTakeDamage = false;

        //// ������� ���� ��ŭ ü���� ����
        Hp -= damage;

        // GameManager���� ü�� ��������
        GameManager.instance.RefreshHp(gameObject.tag, this);

        yield return new WaitForSeconds(damageCooldown);

        CanTakeDamage = true;
    }

    public override void Hit(int damage)
    {
        if (Hp > 0 && CanTakeDamage)
        {
            // ����� ȿ��
            StartCoroutine(IsHitCoroutine(damage));
        }
    }

    private void Start()
    {
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        Slider healthBar = healthBarObject.GetComponentInChildren<Slider>();
        GameManager.instance.RegisterEnemyHealthBar(this, healthBar);
    }
}
