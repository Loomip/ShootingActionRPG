using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_EnemyState
{
    Idle,       // ��û���
    Run,        // �ɾ�ٴϴ� ����
    Attack,     // ���� ����
    Hit,        // ��Ʈ ����
    Knockdown, // �˴ٿ� ����
    WakeUp, // �Ͼ�� ����
    Die         // ���� ����
}

public enum e_MenuType
{
    None,
    Face,   // ��
    Top,    // ����
    Length
}
