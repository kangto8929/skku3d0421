using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum EPopupType
{
    UI_OptionPopup,
    UI_CreditPopup
}

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    public UI_ReadyGo UIReadyGo;

    [Header("�˾� UI ����")]
    public List<UI_Popup> Popups; // ��� �˾��� �����ϴµ�

    private List<UI_Popup> _openedPopups = new List<UI_Popup>(); // null�� �ƴ����� ����ִ� ����Ʈ

    private void Awake()
    {
        Instance = this;
    }

    public void Open(EPopupType type, Action closeCallback = null)
    {
        Open(type.ToString(), closeCallback);
    }

    private void Open(string popupName, Action closeCallback)
    {
        foreach (UI_Popup popup in Popups)
        {
            if (popup.gameObject.name == popupName)
            {
                popup.Open(closeCallback);
                // �˾��� �� ������ ��´�.
                _openedPopups.Add(popup);
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIReadyGo.ReadyGoPanel.activeSelf == false)
        {
            ClosePopUp();
        }
    }


    public void ClosePopUp()
    {
        if (_openedPopups.Count > 0)
        {

            while (true)
            {
                bool opend = _openedPopups[_openedPopups.Count - 1].isActiveAndEnabled;
                _openedPopups[_openedPopups.Count - 1].Close();
                _openedPopups.RemoveAt(_openedPopups.Count - 1);

                if (opend || _openedPopups.Count == 0)
                {
                    return;
                }
            }

        }
        else
        {

            GameManager.Instance.Pause();
        }
    }
}