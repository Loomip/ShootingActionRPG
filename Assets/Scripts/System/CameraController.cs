using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ī�޶�
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    // ������
    [SerializeField] private Transform benchmark;

    // ���� ������
    [SerializeField] private float radius = 10f;
    // ȸ�� �ӵ�
    [SerializeField] private float speed = 10f;
    // ī�޶� ���� ������
    [SerializeField] private float heightOffset = 5f;

    // ���� ����
    private float angle = 0f;

    private void Update()
    {
        CameraTurn();
    }

    private void CameraTurn()
    {
        // ���� ����
        angle += speed * Time.deltaTime;
        if (angle >= 360f)
        {
            angle -= 360f;
        }

        // ���ο� ��ġ ���
        float x = benchmark.position.x + Mathf.Cos(angle) * radius;
        float z = benchmark.position.z + Mathf.Sin(angle) * radius;
        float y = benchmark.position.y + heightOffset; // ���������� ���� ��ġ ����

        // ī�޶� ��ġ ����
        virtualCamera.transform.position = new Vector3(x, y, z);

        // ī�޶� �������� �ٶ󺸵��� ȸ��
        virtualCamera.transform.LookAt(new Vector3(benchmark.position.x, benchmark.position.y, benchmark.position.z));
    }
}
