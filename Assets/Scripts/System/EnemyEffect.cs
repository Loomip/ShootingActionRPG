using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    // 스킬 공격력을 전달받을 계수
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 Player이거나 땅이면 프리펩 제거
        if (other.CompareTag("Player"))
        {
            // 히트 판정
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
