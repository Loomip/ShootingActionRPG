using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    // 체력을 담을 변수
    [SerializeField] private int hp;
    public int Hp { get => hp; set => hp = value; }

    // 데미지 쿨시간 
    [SerializeField] private float damageCooldown;

    // 히트 되면 바뀔 몸 메터리얼
    protected SkinnedMeshRenderer meshs;

    // 맞았는지 
    private bool canTakeDamage = true;
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    public abstract void Hit(int damage);

    private void Start()
    {
        meshs = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    protected IEnumerator DamagerCoolDoun()
    {
        Material[] materialsCopy = meshs.materials;

        // 각 머티리얼의 색상을 변경
        for (int i = 0; i < materialsCopy.Length; i++)
        {
            materialsCopy[i].color = Color.red;
        }

        meshs.materials = materialsCopy;

        // 맞는 사운드
        //SoundManager.instance.PlaySfx(e_Sfx.Hit);

        yield return new WaitForSeconds(0.2f);

        materialsCopy = meshs.materials;

        // 각 머티리얼의 색상을 변경
        for (int i = 0; i < materialsCopy.Length; i++)
        {
            materialsCopy[i].color = Color.white;
        }

        meshs.materials = materialsCopy;
    }


    protected IEnumerator IsHitCoroutine(int damage)
    {
        CanTakeDamage = false;

        //// 대미지가 들어온 만큼 체력을 깍음
        Hp -= damage;

        //// UImanager에서 체력 리프레쉬
        //UIManager.instance.RefreshHp(gameObject.tag, this);

        yield return new WaitForSeconds(damageCooldown);

        CanTakeDamage = true;
    }
}
