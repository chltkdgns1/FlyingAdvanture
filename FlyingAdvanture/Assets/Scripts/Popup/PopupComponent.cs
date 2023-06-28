using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupComponent : BackKeyHandler
{
    public Action closeCallBack = null;

    #region 팝업 스택 static
    static Dictionary<string, GameObject> popupCache = new Dictionary<string, GameObject>();
    static Dictionary<string, PopupComponent> popupDic = new Dictionary<string, PopupComponent>();
    static GameObject canvasObject = null;
    #endregion

    #region 팝업 스택 static
    static public T PopupShow<T>(string popupPath) where T : PopupComponent
    {
        return SetPopup<T>(popupPath);
    }

    static public T PopupShow<T>(string popupPath, string title, string content, bool isTweenMove = false) where T : PopupComponent
    {
        SetPopupString(title, content);
        return SetPopup<T>(popupPath);
    }

    static void SetPopupString(string title, string content)
    {

    }

    public void TweenMoveUpPopup(float yPos = -100, float duration = 1f, bool isBack = false, Action act = null)
    {
        UIEffectManager.PrintPopup(gameObject, transform.position + new Vector3(0, yPos, 0),
            transform.position, 1f, isBack).OnComplete(() =>
            {
                act?.Invoke();
            });
    }

    static bool CheckExistPopup<T>(T popup) where T : PopupComponent
    {
        return isExistPopup<T>(popup);
    }

    static T SetPopup<T>(string popupPath) where T : PopupComponent
    {
        if (canvasObject == null)
        {
            canvasObject = GameObject.Find("Canvas");
        }

        GameObject popupPrefabs = null;
        if (popupCache.ContainsKey(popupPath))
        {
            popupPrefabs = popupCache[popupPath];
        }
        else
        {
            popupPrefabs = Resources.Load<GameObject>(popupPath);
            popupCache.Add(popupPath, popupPrefabs);
        }

        var popup = Instantiate(popupPrefabs, canvasObject.transform).transform;
        popup.SetAsLastSibling();

        AddPopup<T>(popup.GetComponent<T>());
        return popup.GetComponent<T>();
    }

    static void AddPopup<T>(T popup) where T : PopupComponent
    {
        T checkPopup = isExistPopup<T>(popup);
        if (checkPopup != null)
            checkPopup.OnClose();

        string key = GetPopupKey<T>(popup);
        popupDic[key] = popup;
        popup.transform.SetAsLastSibling();
    }

    static T isExistPopup<T>(T popup) where T : PopupComponent
    {
        string key = typeof(T).Name + popup.gameObject.name;
        if (popupDic.ContainsKey(key))
            return popupDic[key] as T;
        return null;
    }

    public override void OnClose()
    {
        closeCallBack?.Invoke();
        Type type = GetType();
        string key = type.Name + gameObject.name;
        popupDic.Remove(key);
        base.OnClose();
    }

    static public bool IsEmpty()
    {
        return popupDic.Count == 0;
    }

    static public string GetPopupKey<T>(T popup) where T : PopupComponent
    {
        return typeof(T).Name + popup.name;
    }
    #endregion
}
