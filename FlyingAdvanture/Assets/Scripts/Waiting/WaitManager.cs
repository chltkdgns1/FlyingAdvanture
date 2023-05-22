using System;
using System.Collections;
using UnityEngine;

public class WaitManager : MonoSingleTon<WaitManager>
{
    protected override void Init()
    {

    }

    public void StartWait(float fTime, Action act = null)
    {
        StartCoroutine(Wait(fTime, act));
    }

    IEnumerator Wait(float fTime, Action act)
    {
        yield return new WaitForSecondsRealtime(fTime);
        act?.Invoke();
    }
}
