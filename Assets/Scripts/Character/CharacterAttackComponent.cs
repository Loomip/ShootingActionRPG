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

    // ������ ���� ȿ�� ���η����� ������Ʈ
    [SerializeField] private GameObject laserGunLinePrefab;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
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

    // �����
    public void BulletShot()
    {
        // ������ �������� ����
        Vector3 offset = Random.insideUnitSphere * 0.2f;
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

    // ��������
    private void LaserGun()
    {
        // ���� ������ ������ �ν��Ͻ�ȭ
        GameObject laserLineInstance = Instantiate(laserGunLinePrefab);
        LineRenderer laserLineRenderer = laserLineInstance.GetComponent<LineRenderer>();
        Laser laser = laserLineInstance.GetComponent<Laser>();

        // LineRenderer ����
        laserLineRenderer.positionCount = 2;
        laserLineRenderer.SetPosition(0, bulletPos.position);

        // ������ ����� ó��
        Vector3 direction = bulletPos.forward;
        laser.CheckForDamage(bulletPos.position, direction);

        // ������ �ִ� �Ÿ����� ���� ����
        laserLineRenderer.SetPosition(1, bulletPos.position + (direction * laser.LaserGunRange));

        // ���� �ð� �� ���� ������ ����
        Destroy(laserLineInstance, 0.1f);
    }
}
