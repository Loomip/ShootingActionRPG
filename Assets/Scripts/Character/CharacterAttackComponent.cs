using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    // �Ѿ� ������
    [SerializeField] private GameObject bulletPrefab;

    // �Ѿ��� ���� ��ġ 
    [SerializeField] private Transform bulletPos;

    // �ѽ�� ��ƼŬ
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
        // ������ �������� ����
        Vector3 offset = Random.insideUnitSphere * 0.2f;

        // �Ѿ��� ����
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position + offset, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        rigidbody.velocity = bulletPos.forward * 20f;
    }
}
