using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    // 사망 완료 처리 시간
    [SerializeField] protected float time;
    [SerializeField] protected float deathDelayTime;

    // 사망 처리 이펙트
    [SerializeField] protected GameObject destroyParticlePrefab;

    public override void EnterState(e_EnemyState state)
    {
        // 이동 중지
        nav.isStopped = true;

        gameObject.layer = 0;

        //SoundManager.instance.PlaySfx(e_Sfx.EnemyDie);

        animator.SetInteger("state", (int)state);

        animator.SetBool("isDeath", true);
    }

    public override void UpdateState()
    {
        time += Time.deltaTime;
        levelManager.OnMonsterDefeated();

        // 사망 처리 지연시간이 지났다면
        if (time >= deathDelayTime)
        {
            Instantiate(destroyParticlePrefab, transform.position, destroyParticlePrefab.transform.rotation);
            Destroy(gameObject);
        }
    }

    public override void ExitState()
    {

    }


}
