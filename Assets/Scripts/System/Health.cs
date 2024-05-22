using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    // ü���� ���� ����
    [SerializeField] private int hp;
    public int Hp { get => hp; set => hp = value; }

    // ������ ��ð� 
    [SerializeField] private float damageCooldown;

    // ��Ʈ �Ǹ� �ٲ� �� ���͸���
    protected SkinnedMeshRenderer meshs;

    // �¾Ҵ��� 
    private bool canTakeDamage = true;
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    public abstract void Hit(int damage);

    private void Start()
    {
        meshs = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    protected IEnumerator DamagerCoolDoun()
    {
        Material[] materialsCopy = meshs.materials;

        // �� ��Ƽ������ ������ ����
        for (int i = 0; i < materialsCopy.Length; i++)
        {
            materialsCopy[i].color = Color.red;
        }

        meshs.materials = materialsCopy;

        // �´� ����
        //SoundManager.instance.PlaySfx(e_Sfx.Hit);

        yield return new WaitForSeconds(0.2f);

        materialsCopy = meshs.materials;

        // �� ��Ƽ������ ������ ����
        for (int i = 0; i < materialsCopy.Length; i++)
        {
            materialsCopy[i].color = Color.white;
        }

        meshs.materials = materialsCopy;
    }


    protected IEnumerator IsHitCoroutine(int damage)
    {
        CanTakeDamage = false;

        //// ������� ���� ��ŭ ü���� ����
        Hp -= damage;

        //// UImanager���� ü�� ��������
        //UIManager.instance.RefreshHp(gameObject.tag, this);

        yield return new WaitForSeconds(damageCooldown);

        CanTakeDamage = true;
    }
}
