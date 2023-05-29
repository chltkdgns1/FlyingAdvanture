using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IAction
{
    ICharacter characterData;

    public Idle(ICharacter characterData)
    {
        this.characterData = characterData;
    }

    public void ExcuteAction()
    {

    }

    public void OnFinish(Action act) { }
    public void OnTarget(Vector3 pos) { }
    public void OnAccel(Vector3 pos) { }
    public void OnRotationStart(Vector3 pos) { }
    public void OnRotationFinish(Vector3 pos) { }
#if UNITY_EDITOR
    public void OnKeyBoardArrow(KeyCode code)
    {

    }
#endif
}
