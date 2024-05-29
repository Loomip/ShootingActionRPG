using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Laser : MonoBehaviour
{
    // ������ ���ݷ�
    [SerializeField] private int atk;

    // Ÿ�� ���̾� �Ǵ� �ڵ�
    [SerializeField] private LayerMask targetLayer;

    // ������ ����
    [SerializeField] private float laserDistance;

    // ������ �ð�ȭ
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Destroy(gameObject, 0.6f);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * laserDistance, Color.red, 2f);
        LaserStraight();
    }

    public void LaserStraight()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, laserDistance, targetLayer);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * laserDistance);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                var enemyHealth = hit.collider.GetComponent<EHeath>();
                if (enemyHealth != null)
                {
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
