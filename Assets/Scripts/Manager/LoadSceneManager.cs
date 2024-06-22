using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    //������ �ϴ� Scene�� �̸�
    public static string next_SceneName;

    //�ε�â�� ��������
    [SerializeField] Slider loading_Gauge;

    //�ε� â�� �ƴ� �ٸ� ������ �ٸ� ���� ������� �Ѿ �� ���
    public static void LoadScene(string SceneName)
    {
        next_SceneName = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        // LoadSceneAsync: LoadScene ���� �����ϰ� �ҷ��� (�ε��� �������� ����)
        AsyncOperation op = SceneManager.LoadSceneAsync(next_SceneName.ToString());
        //���� �ٷ� �Ѿ�� �ʰ�
        op.allowSceneActivation = false;
        float timer = 0f;
        //�츮�� ���� �� ���δ� �ҷ��� ��������, isDone = �Ϻ��ϰ� �ҷ������� true
        while (!op.isDone)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            //�ε��߿��� �ð��� ����
            if (op.progress < 0.9f)
            {
                loading_Gauge.value = Mathf.Lerp(loading_Gauge.value, 1f, timer);
                if (loading_Gauge.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            //�ε��� ������ ���� �ҷ���
            else
            {
                loading_Gauge.value = Mathf.Lerp(loading_Gauge.value, 1f, timer);
                if (loading_Gauge.value >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
