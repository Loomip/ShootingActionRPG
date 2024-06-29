using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] headModels; // �Ӹ� �� �迭
    [SerializeField] private GameObject[] bodyModels; // �� �� �迭

    void Start()
    {
        // ��� �Ӹ��� �� ���� ��Ȱ��ȭ
        foreach (var model in headModels) model.SetActive(false);
        foreach (var model in bodyModels) model.SetActive(false);

        // �Ӹ��� �� �� �߿��� �����ϰ� ����
        GameObject headModel = headModels[Random.Range(0, headModels.Length)];
        GameObject bodyModel = bodyModels[Random.Range(0, bodyModels.Length)];

        // ������ �Ӹ��� �� ���� Ȱ��ȭ
        headModel.SetActive(true);
        bodyModel.SetActive(true);

    }
}
