using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    public void ExcuteAction();
    public void OnTarget(Vector3 pos);
    public void OnAccel(Vector3 pos);
    public void OnRotationStart(Vector3 pos);
    public void OnRotationFinish(Vector3 pos);
    public void OnFinish(Action act);

#if UNITY_EDITOR
    public void OnKeyBoardArrow(KeyCode code);
#endif
}
