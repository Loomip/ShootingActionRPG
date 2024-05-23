using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    // 공격 조이스틱 컴포넌트
    [SerializeField] private VariableJoystick attackJoy;

    // 총알 프리펩
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject strayBulletPrefab;

    // 총알이 나갈 위치 
    [SerializeField] private Transform bulletPos;

    // 총쏘는 파티클
    [SerializeField] private ParticleSystem bulletEffect;

    // 레이저 발포 효과 라인렌더러 컴포넌트
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

    // 기관총
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

    // 유탄발사기
    public void StrayBullet()
    {
        // 총알을 생성
        GameObject bullet = Instantiate(strayBulletPrefab, bulletPos.position, bulletPos.rotation);
        bullet.transform.localRotation *= Quaternion.Euler(90, 0, 0);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = bulletPos.forward * 20f;
    }

    // 레이저건
    private void LaserGun()
    {
        // 라인 렌더러 프리팹 인스턴스화
        GameObject laserLineInstance = Instantiate(laserGunLinePrefab);
        LineRenderer laserLineRenderer = laserLineInstance.GetComponent<LineRenderer>();
        Laser laser = laserLineInstance.GetComponent<Laser>();

        // LineRenderer 설정
        laserLineRenderer.positionCount = 2;
        laserLineRenderer.SetPosition(0, bulletPos.position);

        // 레이저 대미지 처리
        Vector3 direction = bulletPos.forward;
        laser.CheckForDamage(bulletPos.position, direction);

        // 레이저 최대 거리까지 라인 설정
        laserLineRenderer.SetPosition(1, bulletPos.position + (direction * laser.LaserGunRange));

        // 일정 시간 후 라인 렌더러 삭제
        Destroy(laserLineInstance, 0.1f);
    }
}
