using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // ��ų ���ݷ��� ���޹��� ���
    private int atk;
    public int Atk { get => atk; set => atk = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EHeath>().Hit(atk);
            other.GetComponent<EnemyFSMController>().Hit();
        }
    }
}
