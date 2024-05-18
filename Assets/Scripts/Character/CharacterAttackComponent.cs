using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    // 총알 프리펩
    [SerializeField] private GameObject bulletPrefab;

    // 총알이 나갈 위치 
    [SerializeField] private Transform bulletPos;

    // 총쏘는 파티클
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
        // 랜덤한 오프셋을 생성
        Vector3 offset = Random.insideUnitSphere * 0.2f;

        // 총알을 생성
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position + offset, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.velocity = bulletPos.forward * 20f;
    }
}
