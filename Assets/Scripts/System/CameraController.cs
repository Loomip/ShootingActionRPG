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
        // 초기 카메라 우선순위 설정
        startCamera.Priority = 10; // gameCamera보다 높은 우선순위
        gameCamera.Priority = 5;
    }

    // 게임 시작 시 gameCamera로 전환할 버튼 기능
    public void StartGame()
    {
        // gameCamera로 전환 (전환 효과 적용)
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f; // 전환 시간
        startCamera.Priority = 5; // gameCamera보다 낮은 우선순위
        gameCamera.Priority = 10; // 높은 우선순위
    }
}
