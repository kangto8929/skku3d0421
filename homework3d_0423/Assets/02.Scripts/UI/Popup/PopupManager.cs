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

    [Header("팝업 UI 참조")]
    public List<UI_Popup> Popups; // 모든 팝업을 관리하는데

    private List<UI_Popup> _openedPopups = new List<UI_Popup>(); // null은 아니지만 비어있는 리스트

    public GameObject GameOverPanel;

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
                // 팝업을 열 때마다 담는다.
                _openedPopups.Add(popup);
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) 
            && UIReadyGo.ReadyGoPanel.activeSelf == false && GameOverPanel.activeSelf == false)
        {
            ClosePopUp();
        }

        else if(Input.GetMouseButtonDown(0) && GameOverPanel.activeSelf == true)
        {
            GameOverPanel.SetActive(false);
            GameManager.Instance.Restart();
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