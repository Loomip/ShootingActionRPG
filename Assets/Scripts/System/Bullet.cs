using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Ѿ� ���ݷ��� ������ ����
    [SerializeField] private int atk;

    // Ÿ�� ���̾� �Ǵ� �ڵ�
    [SerializeField] List<string> hitLayerNames;

    // �Ѿ��� ����� ���� ��ġ ����
    private Vector3 launchPosition;

    // �Ѿ��� ������ߵ� �ִ� ����
    [SerializeField] private float maxTravelDistance = 100f;

    private void OnTriggerEnter(Collider other)
    {
        // hitLayerName�� �ش��ϴ� ���̾ �ִ� ������Ʈ�� �ִ��� Ȯ��
        if ((other != null && hitLayerNames.Contains(LayerMask.LayerToName(other.gameObject.layer))) || other.tag == "Ground")
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EHeath>().Hit(atk);
                other.GetComponent<EnemyFSMController>().Hit();
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
