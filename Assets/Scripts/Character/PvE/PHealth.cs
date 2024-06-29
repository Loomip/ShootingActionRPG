using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHealth : Health
{
    private Animator animator;

    // ������
    [SerializeField] private GameObject ending;

    // ��Ʈ ��ƼŬ
    [SerializeField] private ParticleSystem hitParticle;

    // �׾�����
    private bool isDie = false;
    public bool IsDie { get => isDie; set => isDie = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            hitParticle.Play();
        }

        else if(Hp <= 0)
        {
            animator.SetTrigger("Death");
            //ending.SetActive(true);
            IsDie = true;
        }
    } 

    public void Knokdown()
    {
        animator.SetTrigger("Knokdown");
    }

    public void Heal(int amount)
    {
        Hp += amount;
        GameManager.instance.RefreshHp(gameObject.tag, this);
    }
}
