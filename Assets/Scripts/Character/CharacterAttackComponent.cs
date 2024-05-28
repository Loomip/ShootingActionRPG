using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    // ���� ���̽�ƽ ������Ʈ
    [SerializeField] private VariableJoystick attackJoy;

    // �Ѿ� ������
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject strayBulletPrefab;

    // �Ѿ��� ���� ��ġ 
    [SerializeField] private Transform bulletPos;

    // �ѽ�� ��ƼŬ
    [SerializeField] private ParticleSystem bulletEffect;

    // ���� ������ ���� ���Ⱑ �ٲ� �迭
    [SerializeField] private GameObject[] weaponObjects;

    private PHealth health;

    private void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<PHealth>();
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (health.IsDie) return;

        //// ���� ���̽�ƽ �Է� ó��
        //float hAttackJoy = attackJoy.Horizontal;
        //float vAttackJoy = attackJoy.Vertical;

        //// ���� ���̽�ƽ�� ���������� Ȯ��
        //if (hAttackJoy != 0 || vAttackJoy != 0)
        //{
        //    // ĳ���� ȸ�� ó��
        //    float angle = Mathf.Atan2(hAttackJoy, vAttackJoy) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}

        //// ���� �ִϸ��̼� ���
        //animator.SetBool("isAtteck", hAttackJoy != 0 || vAttackJoy != 0);

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("isAtteck", true);
        }
        else
        {
            animator.SetBool("isAtteck", false);
        }
    }

    public void BulletEffect()
    {
        bulletEffect.Play();
    }

    public void ChangeWeapon(string newWeaponType)
    {
        // ��� ���⸦ ��Ȱ��ȭ
        foreach (var weapon in weaponObjects)
        {
            weapon.SetActive(false);
        }

        switch (newWeaponType)
        {
            case "MachineGun":
                animator.SetInteger("WeaponType", 0);
                weaponObjects[0].SetActive(true);
                break;
            case "GrenadeLauncher":
                animator.SetInteger("WeaponType", 1);
                weaponObjects[1].SetActive(true);
                break;
        }
    }

    // �����
    public void BulletShot()
    {
        // ������ �������� ����
        Vector3 offset = Random.insideUnitSphere * 0.1f;
        // �Ѿ��� ����
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position + offset, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = bulletPos.forward * 20f;
    }

    // ��ź�߻��
    public void StrayBullet()
    {
        // �Ѿ��� ����
        GameObject bullet = Instantiate(strayBulletPrefab, bulletPos.position, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = bulletPos.forward * 20f;
    }
}
