using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    // ��� �Ϸ� ó�� �ð�
    protected float time;
    [SerializeField] protected float deathDelayTime;

    // ��� ó�� ����Ʈ
    [SerializeField] protected GameObject destroyParticlePrefab;

    public override void EnterState(e_EnemyState state)
    {
        // �̵� ����
        nav.isStopped = true;

        col.isTrigger = true;

        gameObject.layer = 0;

        //SoundManager.instance.PlaySfx(e_Sfx.EnemyDie);

        animator.SetInteger("state", (int)state);

        animator.SetBool("isDeath", true);

        levelManager.OnMonsterDefeated();

        // ��� ����
        controller.DropGold();
    }

    public override void UpdateState()
    {
        time += Time.deltaTime;

        // ��� ó�� �����ð��� �����ٸ�
        if (time >= deathDelayTime)
        {
            // ��� ����Ʈ ����
            Instantiate(destroyParticlePrefab, transform.position, destroyParticlePrefab.transform.rotation);
            // ���� ������ ����
            controller.DropItem();
            // ��ü �ı�
            Destroy(gameObject);
        }
    }

    public override void ExitState()
    {

    }
}
