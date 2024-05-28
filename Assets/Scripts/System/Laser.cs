using UnityEngine;

public class Laser : MonoBehaviour
{
    // 레이저 공격력
    [SerializeField] private int atk;

    // 타겟 레이어 판단 코드
    [SerializeField] private LayerMask targetLayer;

    // 레이저 길이
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
