using System.Collections;
using System.Collections.Generic;

public class EHeath : Health
{
    public override void Hit(int damage)
    {
        if (Hp > 0 && CanTakeDamage)
        {
            // ����� ȿ��
            StartCoroutine(IsHitCoroutine(damage));
            StartCoroutine(DamagerCoolDoun());
        }
    }
}
