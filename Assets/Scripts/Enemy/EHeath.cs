using System.Collections;
using System.Collections.Generic;

public class EHeath : Health
{
    public override void Hit(int damage)
    {
        if (Hp > 0 && CanTakeDamage)
        {
            // 대미지 효과
            StartCoroutine(IsHitCoroutine(damage));
            StartCoroutine(DamagerCoolDoun());
        }
    }
}
