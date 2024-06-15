using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    // ��ų ���ݷ��� ���޹��� ���
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� Player�̰ų� ���̸� ������ ����
        if (other.CompareTag("Player"))
        {
            // ��Ʈ ����
            //PHealth pHealth = other.GetComponent<PHealth>();

            //if (pHealth != null)
            //{
            //    pHealth.Hit(atk);
            //}
        }

        StartCoroutine(EffectDestroy());
    }

    IEnumerator EffectDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
