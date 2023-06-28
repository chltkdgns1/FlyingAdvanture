using System;
using UnityEngine;

public class WaitLoadingManager : MonoSingleTon<WaitLoadingManager>
{
    protected override void Init()
    {

    }

    public void StartWaitLoading(float duration, Action act)
    {
        if (WaitManager.Instance == null) 
            return;

        LoadingUI.Instance.isActive = true;
        act += SetEraseLoadingBack;
        WaitManager.Instance.StartWait(duration, act);
    }

    public void Clear()
    {
        WaitManager.Instance.StopAllCoroutines();
    }

    public void SetEraseLoadingBack() 
    {
        LoadingUI.Instance.isActive = false;
    }
}

