using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : IAction
{
    ICharacter characterData;
    Action onFinish;
    bool isFinish;
    public Collisions(ICharacter characterData)
    {
        this.characterData = characterData;
    }

    public void ExcuteAction()
    {
        if (isFinish)
        {
            onFinish();
            isFinish = false;
            return;
        }
    }
    public void OnTarget(Vector3 pos) { }
    public void OnAccel(Vector3 pos) { }
    public void OnFinish(Action act)
    {
        onFinish = act;
    }
}
