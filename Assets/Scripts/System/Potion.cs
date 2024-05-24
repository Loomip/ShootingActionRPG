using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // ü�� �ö� ��
    [SerializeField] private int hpAmount;

    // �ߴ� ����� �ӵ�
    [SerializeField] private float floatSpeed;
    // �ߴ� ����� ����
    [SerializeField] private float floatHeight;
    // �������� �ʱ� ��ġ�� ����
    private Vector3 originalPosition;

    // �������� �ı��Ǳ������ �ð� (��)
    [SerializeField] private float destroyTime;

    // ���� �ð� ��ȣ�ۿ��� ������ �˾Ƽ� �ı��Ǵ� ����
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void Start()
    {
        // �ʱ� ��ġ ����
        originalPosition = transform.position;

        // ���� �ð� �� ������ �ı�
        StartCoroutine(DestroyAfterTime(destroyTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PHealth pHealth = other.GetComponent<PHealth>();
            if(pHealth != null )
            {
                pHealth.Heal(hpAmount);
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        // �ð��� ���� y�� ��ġ ����
        float newY = originalPosition.y + Mathf.Abs(Mathf.Sin(Time.time * floatSpeed) * floatHeight);
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
