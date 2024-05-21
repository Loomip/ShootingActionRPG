using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ŭ�� : ���� ������ �ٲ��ִ°�
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DoAwake();
    }

    protected virtual void DoAwake() 
    {
    
    }
}
