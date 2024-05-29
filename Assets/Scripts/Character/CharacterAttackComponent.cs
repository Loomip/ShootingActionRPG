using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    // 공격 조이스틱 컴포넌트
    [SerializeField] private VariableJoystick attackJoy;

    // 총알 오브젝트들
    [SerializeField] private GameObject[] shootersPrefab;

    // 총알이 나갈 위치 
    [SerializeField] private Transform bulletPos;
    public Transform BulletPos { get => bulletPos; set => bulletPos = value; }

    // 총쏘는 파티클
    [SerializeField] private ParticleSystem bulletEffect;

    // 공격 종류에 따른 무기가 바뀔 배열
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

        //// 공격 조이스틱 입력 처리
        //float hAttackJoy = attackJoy.Horizontal;
        //float vAttackJoy = attackJoy.Vertical;

        //// 공격 조이스틱이 움직였는지 확인
        //if (hAttackJoy != 0 || vAttackJoy != 0)
        //{
        //    // 캐릭터 회전 처리
        //    float angle = Mathf.Atan2(hAttackJoy, vAttackJoy) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}

        //// 공격 애니메이션 재생
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
        // 모든 무기를 비활성화
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
            case "LaserGun":
                animator.SetInteger("WeaponType", 2);
                weaponObjects[2].SetActive(true);
                break;
        }
    }

    // 기관총
    public void BulletShot()
    {
        // 랜덤한 오프셋을 생성
        Vector3 offset = Random.insideUnitSphere * 0.1f;
        // 총알을 생성
        GameObject bullet = Instantiate(shootersPrefab[0], BulletPos.position + offset, BulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = BulletPos.forward * 20f;
    }

    // 유탄발사기
    public void StrayBullet()
    {
        // 총알을 생성
        GameObject bullet = Instantiate(shootersPrefab[1], BulletPos.position, BulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = BulletPos.forward * 20f;
    }

    // 레이저 건
    public void LaserShot()
    {
        GameObject laserLineInstance = Instantiate(shootersPrefab[2], BulletPos.position, BulletPos.rotation);
    }
}
