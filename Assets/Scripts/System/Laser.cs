using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // 총알 공격력을 전달할 변수
    [SerializeField] private int atk;
    public int Atk { get => atk; set => atk = value; }

    // 레이저 발포 길이
    [SerializeField] private float laserGunRange;
    public float LaserGunRange { get => laserGunRange; set => laserGunRange = value; }

    // 발포 피격 충돌 레이어 마스크
    [SerializeField] private LayerMask laserGunshootableMask;
    public LayerMask LaserGunshootableMask { get => laserGunshootableMask; set => laserGunshootableMask = value; }

    public void CheckForDamage(Vector3 origin, Vector3 direction)
    {
        // 레이캐스트 시작 위치 설정
        Ray laserGunshootRay = new Ray(origin, direction);

        // 발포 레이캐스트 충돌(피격) 체크
        RaycastHit[] hits = Physics.RaycastAll(laserGunshootRay, laserGunRange, laserGunshootableMask);

        foreach (var hit in hits)
        {
            // 피격 당한 몬스터의 피격 처리
            EHeath enemyHealth = hit.collider.GetComponent<EHeath>();
            EnemyFSMController enemyFSMController = hit.collider.GetComponent<EnemyFSMController>();
            if (enemyHealth != null && enemyFSMController != null)
            {
                enemyHealth.Hit(Atk);
                enemyFSMController.Hit();
            }
        }
    }
}
