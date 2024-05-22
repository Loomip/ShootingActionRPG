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

    public override void Hit(int damage)
    {
        if (Hp > 0 && CanTakeDamage)
        {
            // ����� ȿ��
            StartCoroutine(IsHitCoroutine(damage));
            StartCoroutine(DamagerCoolDoun());
            hitParticle.Play();
        }
        else
        {
            animator.SetTrigger("Death");
            //ending.SetActive(true);
            IsDie = true;
        }
    }
}
