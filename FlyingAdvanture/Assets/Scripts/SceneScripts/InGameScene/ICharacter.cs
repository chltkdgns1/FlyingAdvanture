using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public Transform GetTransform();
    public Rigidbody GetRigidBody();
    public CustomRigidMove GetCustomRigidMove();
    public EncryFloat GetSpeed();
}