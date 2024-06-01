using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryMenuController : MonoBehaviour
{
    // �κ��丮 �޴� ��ư ����Ʈ
    List<UIInventoryMenuButton> m_menuList = new List<UIInventoryMenuButton>();

    // �κ��丮 �޴� ���� ������Ʈ ����Ʈ
    [SerializeField] List<GameObject> m_menu = new List<GameObject>();

    // �⺻������ Ȱ��ȭ�� �޴� Ÿ��
    private e_MenuType CurMenu = e_MenuType.Face;

    // ���� �޴� Ÿ���� ��ȯ�ϴ� ������Ƽ
    public e_MenuType CURMENU
    {
        get => CurMenu;
    }

    // ��ư�� ��ġ�� ��ġ
    [SerializeField] Transform buttonTransform;

    // ��ư ������
    [SerializeField] GameObject button;

    // �޴� �������� ��ġ�� ��ġ
    [SerializeField] Transform meunTransform;

    // �κ��丮�� ǥ���ϴ� �޼���
    public void InvenShow()
    {
        Initialize();
    }

    // �ʱ�ȭ �޼���
    private void Initialize()
    {
        // �޴� ��ư �ʱ�ȭ
        InitializeMenuButton();

        // �޴� �ʱ�ȭ
        InitializeMenu(CurMenu);
    }

    // �޴� ��ư�� �ʱ�ȭ�ϴ� �޼���
    private void InitializeMenuButton()
    {
        // �޴� ����Ʈ�� ��������� ��ư�� ����
        if (m_menuList.Count <= 0)
        {
            for (var i = e_MenuType.None + 1; i < e_MenuType.Length; ++i)
            {
                // ��ư�� �����ϰ� UIInventoryMenuButton ������Ʈ�� ������
                UIInventoryMenuButton b = Instantiate(button, buttonTransform).GetComponent<UIInventoryMenuButton>();
                // ������ ��ư�� ����Ʈ�� �߰�
                m_menuList.Add(b);
            }
        }

        // �� ��ư�� �ʱ�ȭ
        for (var i = e_MenuType.None + 1; i < e_MenuType.Length; ++i)
        {
            var index = (int)i - 1;
            // ��ư �ʱ�ȭ
            m_menuList[index].InitButton(i, OnClickButtonCallback);
        }
    }

    // �޴��� �ʱ�ȭ�ϴ� �޼���
    private void InitializeMenu(e_MenuType menuType)
    {
        // ��� �޴��� ��Ȱ��ȭ
        SetActiveAll(false);

        // �޴� �ε��� ���
        int menuIndex = (int)menuType - 1;

        // �޴� �ε����� ��ȿ�� ���� ���� ������
        if (menuIndex >= 0 && menuIndex < m_menu.Count)
        {
            // �޴� �ν��Ͻ� ����
            GameObject menuInstance = null;
            switch (menuType)
            {
                case e_MenuType.Face:
                    menuInstance = m_menu[menuIndex];
                    break;
                case e_MenuType.Top:
                    menuInstance = m_menu[menuIndex];
                    break;
            }

            // �ν��Ͻ��� �����ϸ� Ȱ��ȭ
            if (menuInstance != null)
            {
                menuInstance.SetActive(true);
            }
            else
            {
                Debug.Log("�޴� ���� " + menuType.ToString() + "�� ���� �޴� �ν��Ͻ��� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.Log("�޴� ���� " + menuType.ToString() + "�� ���� �޴� �ε����� �ùٸ��� �ʽ��ϴ�.");
        }
    }

    // ��ư Ŭ�� �ݹ� �޼���
    public void OnClickButtonCallback(e_MenuType menuType)
    {
        // ���� �޴��� menuType���� ����
        CurMenu = menuType;
        // �޴� �ʱ�ȭ
        Initialize();
    }

    // ��� �޴��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �޼���
    private void SetActiveAll(bool enable)
    {
        // m_menu�� null�� �ƴϸ�
        if (m_menu != null)
        {
            // m_menu�� ��� ��ҿ� ����
            for (int i = m_menu.Count - 1; i >= 0; i--)
            {
                // ��Ұ� null�� �ƴϸ�
                if (m_menu[i] != null)
                {
                    m_menu[i].SetActive(enable);
                }
                else
                {
                    // ��Ұ� null�̸� ����Ʈ���� ����
                    m_menu.RemoveAt(i);
                }
            }
        }

        // ���� �޴��� None�� �ƴϸ�
        if (CurMenu != e_MenuType.None)
        {
            // �޴� �ε��� ���
            int menuIndex = (int)CurMenu;
            // �޴� �ε����� ��ȿ�� ���� ���� ������
            if (menuIndex >= 0 && menuIndex < m_menu.Count)
            {
                // �޴� �ν��Ͻ� ����
                GameObject menuinstanceance = m_menu[menuIndex];
                menuinstanceance.SetActive(enable);
            }
            else
            {
                Debug.Log("�޴� ���� " + CurMenu.ToString() + "�� ���� �޴� �ν��Ͻ��� ã�� �� �����ϴ�.");
            }
        }
    }

    private void Start()
    {
        SetActiveAll(true);
        Initialize();
    }
}