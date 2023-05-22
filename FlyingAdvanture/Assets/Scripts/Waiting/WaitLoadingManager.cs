using System;
using UnityEngine;

public class WaitLoadingManager : MonoSingleTon<WaitLoadingManager>
{
    [SerializeField]
    GameObject loadingBack;

    GameObject loadingBackReal;

    protected override void Init()
    {

    }

    public void StartWaitLoading(float duration, Action act)
    {
        if (WaitManager.Instance == null) return;

        if (loadingBackReal == null)
        {
            CreateWaitOb();
        }

        loadingBackReal.SetActive(true);
        act += SetEraseLoadingBack;
        WaitManager.Instance.StartWait(duration, act);
    }

    void CreateWaitOb()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        if (canvasObject == null) return;

        loadingBackReal = Instantiate(loadingBack, canvasObject.transform);
        loadingBackReal.SetActive(true);
    }

    public void Clear()
    {
        WaitManager.Instance.StopAllCoroutines();
    }

    public void SetEraseLoadingBack() 
    { 
        loadingBackReal.SetActive(false);
    }
}

