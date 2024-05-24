using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTransformItem : MonoBehaviour
{
    [SerializeField] private string weaponName;

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
        if (other.tag == "Player")
        {
            CharacterAttackComponent attackComponent = other.GetComponent<CharacterAttackComponent>();
            if (attackComponent != null)
            {
                attackComponent.ChangeWeapon(weaponName);
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
