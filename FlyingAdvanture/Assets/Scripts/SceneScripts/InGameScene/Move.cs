using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IAction
{
    ICharacter characterData;

    Transform charTrans;
    Rigidbody charRigid;

    EncryFloat speed;

    bool isFinishMove;

    Action onFinish;

    public Move(ICharacter characterData)
    {
        this.characterData = characterData;
        charTrans = characterData.GetTransform();
        charRigid = characterData.GetRigidBody();
        speed = characterData.GetSpeed();
    }

    public void ExcuteAction()
    {
        if (isFinishMove)
        {
            onFinish();
            isFinishMove = false;
            return;
        }
    }

    public void OnTarget(Vector3 pos)
    {
        Vector2 targetVec = pos - TouchScreen.Instance.centerPos;
        Debug.Log("pos : " + pos);

        // 방향과 이동할 거리를 계산함.
    }

    public void OnAccel(Vector3 pos)
    {
        
    }

    public void OnFinish(Action act)
    {
        onFinish = act;
    }
}
