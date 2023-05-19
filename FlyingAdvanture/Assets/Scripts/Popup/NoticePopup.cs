using System;
using UnityEngine;
using UnityEngine.UI;

public class NoticePopup : PopupComponent
{
    public UIButtonEx popupNoticeOkBtn;
    public UIButtonEx popupNoticeCancleBtn;

    public bool IsExit
    {
        set
        {
            var image = GetComponent<Image>();
            if (image == null)
                return;

            if (value == true)
            {
                image.color = new Color(0, 0, 0, 1f);
            }
            else
            {
                image.color = new Color(0, 0, 0, 200f / 255f);
            }
        }
    }

    public void ResetOkAct()
    {
        if(popupNoticeOkBtn == null)
        {
            popupNoticeOkBtn = gameObject?.transform?.GetChild(0)?.GetChild(2)?.GetComponent<UIButtonEx>();
        }

        popupNoticeOkBtn.action = null;
    }

    public void SetOkAct(Action act)
    {
        if (popupNoticeOkBtn == null)
        {
            popupNoticeOkBtn = gameObject?.transform?.GetChild(0)?.GetChild(2)?.GetComponent<UIButtonEx>();
        }

        popupNoticeOkBtn.action = act;
    }

    public void ResetCancleAct()
    {
        if (popupNoticeCancleBtn == null)
        {
            popupNoticeCancleBtn = gameObject?.transform?.GetChild(0)?.GetChild(1)?.GetComponent<UIButtonEx>();
        }

        popupNoticeCancleBtn.action = null;
    }

    public void SetCancleAct(Action act)
    {
        if (popupNoticeCancleBtn == null)
        {
            popupNoticeCancleBtn = gameObject?.transform?.GetChild(0)?.GetChild(1)?.GetComponent<UIButtonEx>();
        }

        popupNoticeCancleBtn.action = act;
    }

    public virtual void OnErase()
    {
        gameObject.SetActive(false);
    }

    public void OnPrint()
    {
        gameObject.SetActive(true);
    }
}
