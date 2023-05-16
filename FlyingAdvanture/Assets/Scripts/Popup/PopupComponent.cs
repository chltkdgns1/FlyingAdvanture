using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupComponent : BackKeyHandler
{
    public Action closeCallBack = null;

    #region 팝업 스택 static
    static Dictionary<string, GameObject> popupCache = new Dictionary<string, GameObject>();
    static Dictionary<Type, PopupComponent> popupDic = new Dictionary<Type, PopupComponent>();
    static GameObject canvasObject = null;
    #endregion

    #region 팝업 스택 static
    static public T PopupShow<T>(string popupPath) where T : PopupComponent
    {
        if (canvasObject == null)
        {
            canvasObject = GameObject.Find("Canvas");
        }
        List<string> splitList = new List<string>();
        UtilManager.Split(splitList, popupPath, '/');

        string popupName = splitList[splitList.Count - 1];

        GameObject popupPrefabs = null;

        if (popupCache.ContainsKey(popupName))
        {
            popupPrefabs = popupCache[popupName];
        }
        else
        {
            popupPrefabs = Resources.Load<GameObject>(popupPath);
            popupCache.Add(popupName, popupPrefabs);
        }

        var popup = Instantiate(popupPrefabs, canvasObject.transform).transform;
        popup.SetAsLastSibling();

        AddPopup<T>(popup.GetComponent<T>());
        return popup.GetComponent<T>();
    }

    static void AddPopup<T>(T popup) where T : PopupComponent
    {
        T checkPopup = isExistPopup<T>();
        if (checkPopup != null)
            checkPopup.OnClose();

        popupDic[typeof(T)] = popup;
        popup.transform.SetAsLastSibling();
    }

    static T isExistPopup<T>() where T : PopupComponent
    {
        var type = typeof(T);
        if (popupDic.ContainsKey(type))
            return popupDic[type] as T;
        return null;
    }

    public override void OnClose()
    {
        closeCallBack?.Invoke();
        Type type = GetType();
        popupDic.Remove(type);
        base.OnClose();
    }

    static public bool IsEmpty()
    {
        return popupDic.Count == 0;
    }
    #endregion
}
