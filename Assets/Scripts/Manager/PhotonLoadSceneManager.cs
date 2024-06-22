using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;

public class PhotonLoadSceneManager : MonoBehaviourPunCallbacks
{
    public static string next_SceneName; // �ε��� ���� �̸�
    public GameObject loadingScreen;
    public Slider loadingBar;

    public static void PhotonLoadScene(string sceneName)
    {
        next_SceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    public void StartLoading()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        // �񵿱� �� �ε� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(next_SceneName.ToString());
        asyncOperation.allowSceneActivation = false;

        // �ε� ȭ�� Ȱ��ȭ
        loadingScreen.SetActive(true);

        // �ε� ���൵ ǥ��
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            // �ε��� �Ϸ�Ǹ� �� Ȱ��ȭ
            if (asyncOperation.progress >= 0.9f && PhotonNetwork.IsConnectedAndReady)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        PhotonNetwork.IsMessageQueueRunning = true;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        // �뿡 �����ϸ� �ε��� �����մϴ�.
        StartLoading();
    }
}
