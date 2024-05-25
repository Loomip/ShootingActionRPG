using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // 스킬 공격력을 전달받을 계수
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    // 폭발 넉백
    private float knockBack = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EHeath>().Hit(atk);
            other.GetComponent<EnemyFSMController>().Hit();

            // 폭발 이펙트와 몬스터 사이의 상대적인 위치를 계산하여 넉백 방향 조정
            Vector3 knockBackDirection = other.transform.position - transform.position;
            knockBackDirection.y = 0f; // y 축은 고려하지 않음
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