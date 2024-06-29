using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections;

public class PhotonLoadSceneManager : MonoBehaviourPunCallbacks
{
    public static string next_SceneName; // �ε��� ���� �̸�
    public Slider loadingBar;

    public static void PhotonLoadScene(string sceneName)
    {
        next_SceneName = sceneName;
        SceneManager.LoadScene("PhotonLoadingScene");
    }
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        PhotonNetwork.IsMessageQueueRunning = false;

        // �񵿱� �� �ε� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(next_SceneName.ToString());
        asyncOperation.allowSceneActivation = false;

        float timer = 0f;

        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            // �ε� ���൵ ǥ��
            if (asyncOperation.progress < 0.9f)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, asyncOperation.progress, timer);
                if (loadingBar.value >= asyncOperation.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1f, timer);
                if (loadingBar.value >= 1.0f)
                {
                    if (PhotonNetwork.IsConnectedAndReady)
                    {
                        asyncOperation.allowSceneActivation = true;
                        PhotonNetwork.IsMessageQueueRunning = true;
                        yield break;
                    }
                }
            }
        }
    }
}
