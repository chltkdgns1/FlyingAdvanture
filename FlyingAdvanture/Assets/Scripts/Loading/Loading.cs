using System;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public virtual void EndLoading(float duration, Action act = null)
    {
        if (act == null) act = OnClose;
        else act += OnClose;
        WaitManager.Instance.StartWait(duration, act);
    }

    void OnClose()
    {
        gameObject.SetActive(false);
    }

    public virtual void StartLoading()
    {
        gameObject.SetActive(true);
    }
}
