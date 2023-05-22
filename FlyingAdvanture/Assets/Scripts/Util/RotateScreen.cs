using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScreen : MonoSingleTon<RotateScreen>
{
    public List<Action<Vector3>> accelerList = new List<Action<Vector3>>();

    protected override void Init()
    {

    }

    private void Update()
    {
        if(Input.accelerationEventCount > 0)
        {
            OnAcceleration();
        }
    }

    void OnAcceleration()
    {
        Vector3 accel = Input.acceleration;
        accel.Normalize();

        int cnt = accelerList.Count; 
        for (int i = 0; i < cnt; i++)
        {
            accelerList[i]?.Invoke(accel);
        }
    }

    public int AddEvent(Action<Vector3> act)
    {
        accelerList.Add(act);
        return accelerList.Count - 1;
    }

    public void DeleteEvent(int index, Action<Vector3> act)
    {
        accelerList[index] -= act;
    }
}
