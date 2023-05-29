using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidMove : MonoBehaviour
{
    public float limitX = 1f;
    public float limitY = 1f;
    public float limitZ = 1f;

    private Vector3 sumForce;

    public void AddForce(Vector3 force)
    {
        sumForce += force;
        SetLimitForce();
    }

    public Vector3 GetForce()
    {
        return sumForce;
    }

    void SetLimitForce()
    {
        float x = Mathf.Min(limitX, sumForce.x);
        float y = Mathf.Min(limitY, sumForce.y);
        float z = Mathf.Min(limitZ, sumForce.z);
        sumForce = new Vector3(x, y, z);
    }
}
