using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    // ��� �Ϸ� ó�� �ð�
    [SerializeField] protected float time;
    [SerializeField] protected float deathDelayTime;

    // ��� ó�� ����Ʈ
    [SerializeField] protected GameObject destroyParticlePrefab;

    public override void EnterState(e_EnemyState state)
    {
        // �̵� ����
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

        // ��� ó�� �����ð��� �����ٸ�
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
