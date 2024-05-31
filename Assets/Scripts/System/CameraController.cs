using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera startCamera;
    public CinemachineVirtualCamera gameCamera;
    public CinemachineBrain cinemachineBrain;

    private void Start()
    {
        // �ʱ� ī�޶� �켱���� ����
        startCamera.Priority = 10; // gameCamera���� ���� �켱����
        gameCamera.Priority = 5;
    }

    // ���� ���� �� gameCamera�� ��ȯ�� ��ư ���
    public void StartGame()
    {
        // gameCamera�� ��ȯ (��ȯ ȿ�� ����)
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f; // ��ȯ �ð�
        startCamera.Priority = 5; // gameCamera���� ���� �켱����
        gameCamera.Priority = 10; // ���� �켱����
    }
}
