using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĳ���� �̸� ǥ�� UI ������ ó��
public class UIBillboard : MonoBehaviour
{
	void LateUpdate()
	{
		transform.LookAt(transform.position + Camera.main.transform.forward);
	}
}
