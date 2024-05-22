using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    // �Ѿ� ������
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject strayBulletPrefab;

    // �Ѿ��� ���� ��ġ 
    [SerializeField] private Transform bulletPos;

    // �ѽ�� ��ƼŬ
    [SerializeField] private ParticleSystem bulletEffect;

    // ���� ���ݷ� 
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
        // ������ �������� ����
        Vector3 offset = Random.insideUnitSphere * 0.2f;

        // �Ѿ��� ����
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position + offset, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        Bullet bullet1 = bullet.GetComponent<Bullet>();

        rigidbody.velocity = bulletPos.forward * 20f;

        bullet1.Atk = bulletAkt;
    }

    public void StrayBullet()
    {
        // �Ѿ��� ����
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        Bullet bullet1 = bullet.GetComponent<Bullet>();

        rigidbody.velocity = bulletPos.forward * 20f;

        bullet1.Atk = bulletAkt;
    }
}
