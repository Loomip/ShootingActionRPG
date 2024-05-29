using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // 스킬 공격력을 전달받을 계수
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EHeath>().Hit(atk);
            other.GetComponent<EnemyFSMController>().Knockdown();
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