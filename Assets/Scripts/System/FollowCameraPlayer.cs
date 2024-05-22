using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraPlayer : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");

        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
    }
}
