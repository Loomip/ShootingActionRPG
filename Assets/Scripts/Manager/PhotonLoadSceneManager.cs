using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;

public class PhotonLoadSceneManager : MonoBehaviourPunCallbacks
{
    public static string next_SceneName; // 로드할 씬의 이름
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

        // 비동기 씬 로딩 시작
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(next_SceneName.ToString());
        asyncOperation.allowSceneActivation = false;

        // 로딩 화면 활성화
        loadingScreen.SetActive(true);

        // 로딩 진행도 표시
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            // 로딩이 완료되면 씬 활성화
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
        // 룸에 입장하면 로딩을 시작합니다.
        StartLoading();
    }
}
