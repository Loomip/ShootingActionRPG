using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayBullet : MonoBehaviour
{
    // ����Ʈ ���ݷ��� ������ ����
    [SerializeField] private int atk;

    // Ÿ�� ���̾� �Ǵ� �ڵ�
    [SerializeField] List<string> hitLayerNames;

    // �Ѿ��� ����� ���� ��ġ ����
    private Vector3 launchPosition;

    // �Ѿ��� ������ߵ� �ִ� ����
    [SerializeField] private float maxTravelDistance = 100f;

    // ���� ����Ʈ
    [SerializeField] private GameObject explosionEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // hitLayerName�� �ش��ϴ� ���̾ �ִ� ������Ʈ�� �ִ��� Ȯ��
        if ((other != null && hitLayerNames.Contains(LayerMask.LayerToName(other.gameObject.layer))) || other.tag == "Ground")
        {
            if (other.tag == "Enemy")
            {
                GameObject effect = Instantiate(explosionEffectPrefab, transform.position - new Vector3(0f, 1f, 0f) , Quaternion.identity);
                Effect effect1 = effect.GetComponent<Effect>();
                effect1.Atk = atk;
            }

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // ���׷� �Ѿ��� ���¾��� ��� �Ѿ��� ��� ����ֱ� ������ ���� ���� �̻����� ��������
        // �Ѿ��� ���� �������
        if (Vector3.Distance(transform.position, launchPosition) > maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }
}
