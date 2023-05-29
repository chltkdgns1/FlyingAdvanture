using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLogic
{
    public void WaitGameStart(Action act);
    public void SetInGameScene(InGameScene scene);
}

public class NomalGameLogic : IGameLogic
{
    private InGameScene scene;
    private float waitStartTime = 5f;

    int waitNumber = 5;

    public void WaitGameStart(Action act)
    {
        scene.StartCoroutine(WaitStart(act));
    }

    IEnumerator WaitStart(Action act)
    {
        yield return Waitor.sec1;
        while (waitNumber > 0)
        {
            scene.startWaitTxt.text = waitNumber.ToString();
            waitNumber--;
            yield return Waitor.sec1;
        }

        act?.Invoke();
    }

    public void SetInGameScene(InGameScene scene)
    {
        this.scene = scene;
    }
}
