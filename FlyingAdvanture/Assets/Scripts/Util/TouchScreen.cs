using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoSingleTon<TouchScreen>
{
    public class MultiTouchData
    {
        public List<Touch> touchList = new List<Touch>();
    }

    public List<Action<Vector3>> touchAct = new List<Action<Vector3>>();
    public List<Action<MultiTouchData>> multiTouchAct = new List<Action<MultiTouchData>>();
    public List<Action<Vector3>> ClickAct = new List<Action<Vector3>>();

    public Vector3 centerPos;

    protected override void Init()
    {
        centerPos = new Vector3(Screen.width / 2f, Screen.height / 2f);
    }

    public void AddEvent(int index, Action<Vector3> act)
    {
        if (touchAct.Count <= index || ClickAct.Count <= index)
            return;

        touchAct[index] += act;
        ClickAct[index] += act;
    }

    public int AddEvent(Action<Vector3> act)
    {
        touchAct.Add(act);
        ClickAct.Add(act);
        return touchAct.Count - 1;
    }

    public int AddMultiTouchEvent(Action<MultiTouchData> act)
    {
        multiTouchAct.Add(act);
        return multiTouchAct.Count - 1;
    }

    public void DeleteEvent(int index, Action<Vector3> act)
    {
        touchAct[index] -= act;
        ClickAct[index] -= act;
    }

    public void DeleteMultiTouchEvent(int index, Action<MultiTouchData> act)
    {
        multiTouchAct[index] -= act;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            OnTouchEvent();
            OnMultiTouchEvent();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseEvent();
        }
    }

    void OnTouchEvent()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            int cnt = touchAct.Count;
            for (int i = 0; i < cnt; i++)
            {
                touchAct[i]?.Invoke(pos);
            }
        }
    }

    void OnMultiTouchEvent()
    {
        var touches = Input.touches;

        MultiTouchData mulTouch = new MultiTouchData();
        for (int i = 0; i < touches.Length; i++)
        {
            mulTouch.touchList.Add(touches[i]);
        }

        int cnt = multiTouchAct.Count;
        for (int i = 0; i < cnt; i++)
        {
            multiTouchAct[i]?.Invoke(mulTouch);
        }
    }

    void OnMouseEvent()
    {
        Vector3 pos = Input.mousePosition;
        int cnt = ClickAct.Count;
        for (int i = 0; i < cnt; i++)
        {
            ClickAct[i]?.Invoke(pos);
        }
    }
}
