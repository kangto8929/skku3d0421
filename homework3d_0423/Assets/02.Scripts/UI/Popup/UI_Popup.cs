using System;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Popup : MonoBehaviour
{
    // �ݹ� �Լ�: � �Լ��� ����س��ٰ� Ư�� ����(�۾��� �Ϸ�� ��)�� ȣ���ϴ� �Լ�
    private Action _closeCallback;

    public void Open(Action closeCallback = null) // 1. ��! �˾�����! �ٵ�! �� ������ ȣ���� �Լ��� ����Ұ�
    {
        _closeCallback = closeCallback;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        _closeCallback?.Invoke();               // 2. �� ������! ��?! ������ ȣ���� �Լ��� ��ϵǾ��ֳ�? �����ؾ���!

        gameObject.SetActive(false);
    }

   
}