using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Photon ���� using ó��
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun.Demo.PunBasics;

public class TitleManager : MonoBehaviourPunCallbacks
{
    // �Ѿ �� �̸�
    [SerializeField] private string SingleMode;
    [SerializeField] private string MultiMode;

    // ���� ���� �ε� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI logMessageText;

    // ���� ��ư
    [SerializeField] private Button startButton;

    private void Awake()
    {
        // ���� �� �ڵ� ����ȭ ��� Ȱ��ȭ
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� �޽��� ť ��� ���� Ȱ��ȭ
        PhotonNetwork.IsMessageQueueRunning = true;

        startButton.interactable = false; // ��ư ��Ȱ��ȭ
    }

    private void Start()
    {
        // ���� ���� Ŭ���忡 ���ӵ� ���°� �ƴϸ�
        if (!PhotonNetwork.IsConnected)
        {
            logMessageText.text = "���� ��Ʈ��ũ ���� �õ�";

            // ���� ��Ʈ��ũ ���� ���� ���� �������� Ŭ���� ������ ������
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void Update()
    {
        if (PhotonNetwork.InLobby)
        {
            logMessageText.text = "���� ��Ʈ��ũ �κ� ���� ���� : " +
                (PhotonNetwork.InLobby ? "���� �Ϸ� (�ο� : " +
                PhotonNetwork.CountOfPlayersOnMaster + ")" : "���� ����");
        }
    }

    // [���� ��Ʈ��ũ �̺�Ʈ �ݹ� �޼ҵ��]

    // ���� Ŭ���� ���� �Ϸ� �̺�Ʈ
    public override void OnConnectedToMaster()
    {
        logMessageText.text = "���� ��Ʈ��ũ ���� �Ϸ�";

        // ���� ���� ��ư Ȱ��ȭ
        startButton.interactable = true;

        // ���� �κ� ������ ���°� �ƴ϶��
        if (!PhotonNetwork.InLobby)
        {
            // ���� Ŭ���� �κ� ������
            PhotonNetwork.JoinLobby();
        }
    }

    // ���� Ŭ���� �κ� ���� �Ϸ� �̺�Ʈ
    public override void OnJoinedLobby()
    {

    }

    // ���� ���� ���� �õ�
    public void JoinRandomRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            // ��뿡 ���� ���� �õ�
            PhotonNetwork.JoinRandomRoom();
        }
    }

    // ���� ���� ���� ���� �̺�Ʈ
    public override void OnJoinedRoom()
    {
        // ���� �� ��ȯ API�� �̿��� ���� ��ȯ��

        // ��Ƽ �÷��� �ε� �� ����
        PhotonLoadSceneManager.PhotonLoadScene(MultiMode);
    }

    // ���� ������ �������� ��� ���� ������ ������ ���� ������
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ���� ������ ������ ��� ����� ���� ������ (�ִ� �������� 10)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 10 });
    }

    public void OnSinggleModeButtonClick()
    {
        LoadSceneManager.LoadScene(SingleMode);
    }

    public void OnMultiModeButtonClick()
    {
        // ���� ���� �� ���� ��� ����
        JoinRandomRoom();
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
