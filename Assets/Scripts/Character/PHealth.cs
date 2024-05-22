using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHealth : Health
{
    private Animator animator;

    // 엔딩씬
    [SerializeField] private GameObject ending;

    // 히트 파티클
    [SerializeField] private ParticleSystem hitParticle;

    // 죽엇는지
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
            // 대미지 효과
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
