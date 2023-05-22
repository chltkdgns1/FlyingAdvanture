using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTestCodeSingleTon : MonoSingleTon<CallbackTestCodeSingleTon>
{
    public Action act;

    protected override void Init()
    {

    }

    public void WaitStart()
    {
        StartCoroutine(CoWaitStart());
    }

    IEnumerator CoWaitStart()
    {
        yield return new WaitForSeconds(5f);
        act?.Invoke();
    }
}
