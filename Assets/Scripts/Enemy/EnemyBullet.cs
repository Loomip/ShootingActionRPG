using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // �߻�ü�� �����ϴ� �ִ� ����
    [SerializeField] private float h;
    // �߷� ���ӵ�
    [SerializeField] private float gravity;
    // ���ݷ� ����
    private int atk;

    public int Atk { get => atk; set => atk = value; }

    public LaunchData CalculateLaunchData(Transform target, Transform projectile)
    {
        // �߻�ü�� ��ǥ�� ������ ���� �� ���� �Ÿ� ���
        float displacementY = target.position.y - projectile.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - projectile.position.x, 0, target.position.z - projectile.position.z);

        // �߻�ü�� ��ǥ���� �����ϴ� �� �ɸ��� �ð� ���
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

        // �߻�ü�� ���� �� ���� �ӵ� ���
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * displacementY);
        Vector3 velocityXZ = displacementXZ / time;

        // �ʱ� �ӵ��� ��ǥ���� �����ϴ� �� �ʿ��� �ð� ��ȯ
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    public struct LaunchData
    {
        // �ʱ� �ӵ�
        public readonly Vector3 initialVelocity;

        // ��ǥ���� �����ϴ� �� �ʿ��� �ð�
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� Player�̰ų� ���̸� ������ ����
        if (other.CompareTag("Player"))
        {
            // ��Ʈ ����
            PHealth pHealth = other.GetComponent<PHealth>();

            if (pHealth != null)
            {
                pHealth.Hit(atk);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }
}
