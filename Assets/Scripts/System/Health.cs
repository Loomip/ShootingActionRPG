using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    // �ִ� ü���� ���� ����
    [SerializeField] private int maxHp;
    public int MaxHp { get => maxHp; set => maxHp = value; }

    // ���� ü���� ���� ����
    [SerializeField] private int hp;
    public int Hp
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp); // ü���� 0�� maxHp ������ ������ �����ǵ��� ����
        }
    }

    // ������ ��ð� 
    [SerializeField] protected float damageCooldown;

    // �¾Ҵ��� 
    private bool canTakeDamage = true;
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    public abstract void Hit(int damage);

    void Awake()
    {
        Hp = maxHp; // ó�� ü���� �ִ� ü������ ����
    }
}
