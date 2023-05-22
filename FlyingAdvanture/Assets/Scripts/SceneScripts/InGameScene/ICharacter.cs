using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public Transform GetTransform();
    public Rigidbody GetRigidBody();
    public EncryFloat GetSpeed();
}