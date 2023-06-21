using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IAction
{
    ICharacter characterData;

    Transform charTrans;
    Rigidbody charRigid;
    CustomRigidMove customRigidMove;

    EncryFloat speed;
    EncryFloat resistanceSpeed;

    bool isFinishMove;

    Action onFinish;

    bool isRotationState = false;
    Vector3 dragePos;

    Quaternion forwardQuat = Quaternion.identity;

    public Move(ICharacter characterData)
    {
        this.characterData = characterData;
        charTrans = characterData.GetTransform();
        charRigid = characterData.GetRigidBody();
        customRigidMove = characterData.GetCustomRigidMove();
        speed = characterData.GetSpeed();

        forwardQuat = Quaternion.Euler(new Vector3(0f, 0f, 0f));

#if UNITY_EDITOR
        resistanceSpeed = new EncryFloat(0.1f);
#else
        // 기울기 값에 대한 적절한 값을 찾아야함
        resistanceSpeed = new EncryFloat(0.01f);
#endif
    }

    public void ExcuteAction()
    {
        var force = customRigidMove.GetForce();
        charTrans.position += force * Time.deltaTime;
        force *= 0.001f;
        customRigidMove.AddForce(-force);

        if (isRotationState == false)
        {
            charTrans.rotation = Quaternion.Lerp(charTrans.rotation, forwardQuat, Time.deltaTime * 1f);
        }
    }

    public void OnTarget(Vector3 pos)
    {
        float centerX = Screen.width / 2f;
        float centerY = Screen.height / 2f;

        pos.x -= centerX;
        pos.y -= centerY;

        var nextPos = new Vector3(pos.x, 0, pos.y);
        nextPos = charTrans.TransformVector(nextPos);
        customRigidMove.AddForce(nextPos * speed.GetFloat() * resistanceSpeed.GetFloat());
    }

    public void OnAccel(Vector3 pos)
    {
        customRigidMove.AddForce(new Vector3(pos.x, 0, pos.y) * speed.GetFloat() * resistanceSpeed.GetFloat());
    }

    public void OnRotationStart(Vector3 pos) 
    {
        float centerX = Screen.width / 2f;
        float centerY = Screen.height / 2f;

        pos.x -= centerX;
        pos.y -= centerY;

        if (isRotationState == false)
        {
            dragePos = pos;
            isRotationState = true;
        }
        else
        {
            Rotation(pos);
        }
    }

    public void Rotation(Vector3 pos)
    {
        Vector3 targetRotation = (pos - dragePos) * GameSettingData.sensitiveAngle * GameSettingData.sensitive * Time.deltaTime;
        charTrans.Rotate(new Vector3(-targetRotation.y, targetRotation.x, 0));
    }

    public void OnRotationFinish(Vector3 pos)
    {
        isRotationState = false;
    }

    public void OnFinish(Action act)
    {
        onFinish = act;
    }

#if UNITY_EDITOR
    public void OnKeyBoardArrow(KeyCode code)
    {
        Vector3 force = Vector3.zero;
        switch (code)
        {
            case KeyCode.RightArrow:
                force = charTrans.TransformVector(Vector3.right);
                customRigidMove.AddForce(force * speed.GetFloat() * resistanceSpeed.GetFloat());
                break;
            case KeyCode.UpArrow:
                force = charTrans.TransformVector(new Vector3(0,0,1));
                customRigidMove.AddForce(force * speed.GetFloat() * resistanceSpeed.GetFloat());
                break;
            case KeyCode.LeftArrow:
                force = charTrans.TransformVector(Vector3.left);
                customRigidMove.AddForce(force * speed.GetFloat() * resistanceSpeed.GetFloat());
                break;
            case KeyCode.DownArrow:
                force = charTrans.TransformVector(new Vector3(0, 0, -1));
                customRigidMove.AddForce(force * speed.GetFloat() * resistanceSpeed.GetFloat());
                break;
        }
    }
#endif
}
