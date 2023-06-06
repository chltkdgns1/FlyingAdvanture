using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidMove : MonoBehaviour
{
    float limitX = 10f;
    float limitY = 10f;
    float limitZ = 10f;

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
        float x = Mathf.Max(-limitX, Mathf.Min(limitX, sumForce.x));
        float y = Mathf.Max(-limitY, Mathf.Min(limitY, sumForce.y));
        float z = Mathf.Max(-limitZ, Mathf.Min(limitZ, sumForce.z));
        sumForce = new Vector3(x, y, z);
    }
}
