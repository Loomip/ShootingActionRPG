using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    // ������Ʈ Ǯ ��ųʸ�
    // * ������Ʈ Ǯ ����Ʈ���� ���� ��ųʸ�
    // - Ű : ���ӿ�����Ʈ����
    // - �� : ����Ʈ ����
    private Dictionary<GameObject, List<GameObject>> objactPoolMap = new Dictionary<GameObject, List<GameObject>>();

    // ������Ʈ Ű ������ ����Ʈ
    [SerializeField] private List<GameObject> prefabstoPool = new List<GameObject>();

    // ������Ʈ Ǯ ������
    [SerializeField] private int poolSize;

    // ������ �������� ���� ������Ʈ Ǯ ����
    private void CreateObjectPool(GameObject prefab, int poolSize)
    {
        // ������ �������� �̹� ������Ʈ Ǯ�� ������ �Ǿ� ���� �ʴٸ�
        if (!objactPoolMap.ContainsKey(prefab))
        {
            // ������Ʈ Ǯ ����Ʈ ��ü ����
            List<GameObject> objectPoolList = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                obj.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
                objectPoolList.Add(obj); // ������Ʈ Ǯ�� ������ ���ӿ�����Ʈ �߰�
            }

            //  ������Ʈ Ǯ ��ųʸ��� ������ ������Ʈ Ǯ �߰�
            objactPoolMap.Add(prefab, objectPoolList);
        }
    }

    // ������ �������� ������Ʈ Ǯ���� �ϳ��� ������Ʈ�� ��ȯ��.
    public GameObject GetObjectFromPool(GameObject prefabKey, Vector3 position, Quaternion rotation)
    {
        // ������Ʈ Ǯ ��ųʸ��� ������ ������ Ű�� ���� ������Ʈ Ǯ ����Ʈ�� �����ϴ����� üũ
        if (objactPoolMap.ContainsKey(prefabKey))
        {
            // ������Ʈ Ǯ ��ųʸ����� ����Ʈ�� ������
            List<GameObject> objectPoolList = objactPoolMap[prefabKey];

            foreach (GameObject obj in objectPoolList)
            {
                // ���� ���ӿ�����Ʈ Ȱ��ȭ �� ���°� �ƴϸ�
                if (!obj.activeInHierarchy)
                {
                    // ������Ʈ�� ��ġ�� ȸ���� �����ϰ� Ȱ��ȭ�ѵ� ������ ��ȯ
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }

            }

            // ���� ���ӿ�����Ʈ�� ������
            GameObject newObj = Instantiate(prefabKey, position, rotation);
            objectPoolList.Add(newObj);
            return newObj;
        }

        Debug.LogError($"������Ʈ Ǯ ��ųʸ��� ������ ��Ұ� ���� : {prefabKey.name}");
        return null;
    }

    // ���ӿ�����Ʈ�� ��ȯ��(��Ȱ��ȭ ó��)
    public void ReturnObjectToPool(GameObject returnObj)
    {
        returnObj.transform.position = Vector3.zero;
        returnObj.transform.rotation = Quaternion.identity;
        returnObj.SetActive(false);
    }

    private void Start()
    {
        // ������Ʈ Ǯ ��ųʸ��� ������Ʈ Ǯ ����Ʈ ����
        foreach (GameObject prefab in prefabstoPool)
        {
            CreateObjectPool(prefab, poolSize);
        }
    }
}
