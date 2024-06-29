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

    protected IEnumerator IsHitCoroutine(int damage)
    {
        CanTakeDamage = false;

        //// 대미지가 들어온 만큼 체력을 깍음
        Hp -= damage;

        // GameManager에서 체력 리프레쉬
        GameManager.instance.RefreshHp(gameObject.tag, this);

        yield return new WaitForSeconds(damageCooldown);

        CanTakeDamage = true;
    }

    public override void Hit(int damage)
    {
        if (Hp > 0 && CanTakeDamage)
        {
            // 대미지 효과
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
