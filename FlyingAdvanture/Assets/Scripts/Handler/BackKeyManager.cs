using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackKeyManager : MonoSingleTon<BackKeyManager>
{
    static List<BackKeyHandler> list = new List<BackKeyHandler>();

    List<BackKeyHandler> tempList = new List<BackKeyHandler>();

    protected override void Init()
    {
        list.Clear();
    }

    BackKeyHandler GetLastHandle()
    {
        if (list == null || list.Count == 0)
            return null;

        return list[list.Count - 1];
    }

    void InputBack()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            CheckKeyDown();
        }
    }

    void CheckKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetBack();
        }
    }

    public void SetBack()
    {
        bool isEmpty = list.Count == 0;

        if (isEmpty)
        {
            if (PlayingGameManager.IsInGame())
            {
                // 인게임일 경우 팝업창을 출력한다.
                //InGameManager.instance?.SetPrintGamePausePopup();
            }
            else
            {
                //var popup = Popup<NoticePopup>.ShowPopup(PopupPath.PopupNotice, StringList.LanguageTable, StringList.ExitGame);
                //popup.SetOkAct(() =>
                //{
                //    UtilManager.Quit();
                //});
            }
        }
        else
        {
            Close(GetLastHandle());
        }
    }

    public void Add(BackKeyHandler handle)
    {
        list.Add(handle);
    }
    
    // BackKey 가 아닌 외부에서 OnClose 를 호출한 경우. isCloed = true;
    public void Delete(BackKeyHandler handle)
    {
        for(int i = list.Count - 1; i >= 0; i--)
        {
            if(list[i] == handle)
            {
                list.RemoveAt(i);
                break;
            }
        }
    }

    void Close(BackKeyHandler handle)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] == handle)
            {
                list[i].OnClose();
                break;
            }
        }
    }

    public void SetLastSibling(BackKeyHandler handle)
    {
        list.Remove(handle);
        list.Add(handle);
        handle.transform.SetAsLastSibling();
    }

}
