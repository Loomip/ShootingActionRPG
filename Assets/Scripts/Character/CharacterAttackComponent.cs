using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    // 총알 프리펩
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject strayBulletPrefab;

    // 총알이 나갈 위치 
    [SerializeField] private Transform bulletPos;

    // 총쏘는 파티클
    [SerializeField] private ParticleSystem bulletEffect;

    // 무기 공격력 
    [SerializeField] private int bulletAkt;


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

    public void ChangeWeapon(string newWeaponType)
    {
        switch (newWeaponType)
        {
            case "Pistol":
                animator.SetInteger("WeaponType", 0);
                break;
            case "Rifle":
                animator.SetInteger("WeaponType", 1);
                break;
            case "Shotgun":
                animator.SetInteger("WeaponType", 2);
                break;
        }
    }

    public void BulletShot()
    {
        // 랜덤한 오프셋을 생성
        Vector3 offset = Random.insideUnitSphere * 0.2f;

        // 총알을 생성
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position + offset, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        Bullet bullet1 = bullet.GetComponent<Bullet>();

        rigidbody.velocity = bulletPos.forward * 20f;

        bullet1.Atk = bulletAkt;
    }

    public void StrayBullet()
    {
        // 총알을 생성
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        Bullet bullet1 = bullet.GetComponent<Bullet>();

        rigidbody.velocity = bulletPos.forward * 20f;

        bullet1.Atk = bulletAkt;
    }
}
