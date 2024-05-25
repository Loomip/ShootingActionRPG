using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // ��ų ���ݷ��� ���޹��� ���
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    // ���� �˹�
    private float knockBack = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EHeath>().Hit(atk);
            other.GetComponent<EnemyFSMController>().Hit();

            // ���� ����Ʈ�� ���� ������ ������� ��ġ�� ����Ͽ� �˹� ���� ����
            Vector3 knockBackDirection = other.transform.position - transform.position;
            knockBackDirection.y = 0f; // y ���� ������� ����
            knockBackDirection.Normalize();

            other.GetComponent<Rigidbody>().AddForce(knockBackDirection * knockBack, ForceMode.Impulse);
        }

        Collider collider = GetComponent<Collider>();
        StartCoroutine(SetOffCollider(collider));
    }

    IEnumerator SetOffCollider(Collider collider)
    {
        yield return null;
        collider.enabled = false;
    }
}