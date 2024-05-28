using UnityEngine;

public class Laser : MonoBehaviour
{
    // ������ ���ݷ�
    [SerializeField] private int atk;

    // Ÿ�� ���̾� �Ǵ� �ڵ�
    [SerializeField] private LayerMask targetLayer;

    // ������ ����
    [SerializeField] private float laserDistance;

    public float LaserDistance { get => laserDistance; set => laserDistance = value; }

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void LaserStraight()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, LaserDistance, targetLayer);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                var enemyHealth = hit.collider.GetComponent<EHeath>();
                if (enemyHealth != null)
                {
                    Debug.Log($"Hit enemy: {hit.collider.gameObject.name}");
                    enemyHealth.Hit(atk);
                }

                var enemyFSMController = hit.collider.GetComponent<EnemyFSMController>();
                if (enemyFSMController != null)
                {
                    enemyFSMController.Hit();
                }
            }
            else
            {
                Debug.LogWarning($"Hit collider is null");
            }
        }
    }
}
