using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    
}

public class Player : MonoBehaviour, ICharacter
{
    IAction action;
    EncryFloat speed = new EncryFloat();

    CustomRigidMove customRigidMove;

    public IAction ActionData
    {
        get { return action; }
        set { action = value; }
    }

    private void Awake()
    {
        speed.value = 2f;
        customRigidMove = GetComponent<CustomRigidMove>();
    }

    private void Update()
    {
        action.ExcuteAction();
    }

    public CustomRigidMove GetCustomRigidMove()
    {
        return customRigidMove;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Rigidbody GetRigidBody()
    {
        return GetComponent<Rigidbody>();
    }
    public EncryFloat GetSpeed()
    {
        return speed;
    }
}
