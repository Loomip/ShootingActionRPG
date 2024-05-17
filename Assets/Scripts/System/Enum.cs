using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_EnemyState
{
    Idle,       // 평시상태
    Run,        // 걸어다니는 상태
    Attack,     // 공격 상태
    Hit,        // 히트 상태
    Die         // 죽음 상태
}
