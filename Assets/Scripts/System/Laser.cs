using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // �Ѿ� ���ݷ��� ������ ����
    [SerializeField] private int atk;
    public int Atk { get => atk; set => atk = value; }

    // ������ ���� ����
    [SerializeField] private float laserGunRange;
    public float LaserGunRange { get => laserGunRange; set => laserGunRange = value; }

    // ���� �ǰ� �浹 ���̾� ����ũ
    [SerializeField] private LayerMask laserGunshootableMask;
    public LayerMask LaserGunshootableMask { get => laserGunshootableMask; set => laserGunshootableMask = value; }

    public void CheckForDamage(Vector3 origin, Vector3 direction)
    {
        // ����ĳ��Ʈ ���� ��ġ ����
        Ray laserGunshootRay = new Ray(origin, direction);

        // ���� ����ĳ��Ʈ �浹(�ǰ�) üũ
        RaycastHit[] hits = Physics.RaycastAll(laserGunshootRay, laserGunRange, laserGunshootableMask);

        foreach (var hit in hits)
        {
            // �ǰ� ���� ������ �ǰ� ó��
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
