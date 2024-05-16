using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // ¾Ö´Ï¸ÞÀÌÅÍ ÄÄÆ÷³ÍÆ®
    private Animator animator;

    // ÃÑ¾Ë ÇÁ¸®Æé
    [SerializeField] private GameObject bulletPrefab;

    // ÃÑ¾ËÀÌ ³ª°¥ À§Ä¡ 
    [SerializeField] private Transform bulletPos;

    // ÃÑ½î´Â ÆÄÆ¼Å¬
    [SerializeField] private ParticleSystem bulletEffect;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        
    }

    public void BulletEffect()
    {
        bulletEffect.Play();
    }

    public void BulletShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.velocity = bulletPos.forward * 20f;
    }
}
