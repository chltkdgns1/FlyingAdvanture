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
    public List<Action<Vector3>> dragDownAct = new List<Action<Vector3>>();
    public List<Action<Vector3>> dragUpAct = new List<Action<Vector3>>();

    public Vector3 centerPos;

    public bool isDragState = false;

    protected override void Init()
    {
        centerPos = new Vector3(Screen.width / 2f, Screen.height / 2f);
    }

    public void AddEvent(int index, Action<Vector3> act)
    {
        if (touchAct.Count <= index)
            return;

        touchAct[index] += act;
    }

    public int AddEvent(Action<Vector3> act)
    {
        touchAct.Add(act);
        return touchAct.Count - 1;
    }

    public int AddDragDownEvent(Action<Vector3> act)
    {
        dragDownAct.Add(act);
        return dragDownAct.Count - 1;
    }

    public int AddDragUpEvent(Action<Vector3> act)
    {
        dragUpAct.Add(act);
        return dragUpAct.Count - 1;
    }

    public int AddMultiTouchEvent(Action<MultiTouchData> act)
    {
        multiTouchAct.Add(act);
        return multiTouchAct.Count - 1;
    }

    public void DeleteEvent(int index, Action<Vector3> act)
    {
        touchAct[index] -= act;
    }

    public void DeleteDragDownEvent(int index, Action<Vector3> act)
    {
        dragDownAct[index] -= act;
    }

    public void DeleteDragUpEvent(int index, Action<Vector3> act)
    {
        dragUpAct[index] -= act;
    }

    public void DeleteMultiTouchEvent(int index, Action<MultiTouchData> act)
    {
        multiTouchAct[index] -= act;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseEvent();
            OnMouseDragUpEvent();

            isDragState = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDragState = true;
            OnMouseDragDownEvent();
        }

        if (isDragState)
        {
            OnMouseDragDownEvent();
        }
#else
        if (Input.touchCount > 0)
        {
            OnTouchEvent();
            OnMultiTouchEvent();
            OnTouchDragDownEvent();
            OnTouchDragUpEvent();
        }
#endif
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

    void OnTouchDragDownEvent()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        {
            int cnt = dragDownAct.Count;
            for (int i = 0; i < cnt; i++)
            {
                dragDownAct[i]?.Invoke(pos);
            }
        }
    }

    void OnTouchDragUpEvent()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Canceled)
        {
            int cnt = dragDownAct.Count;
            for (int i = 0; i < cnt; i++)
            {
                dragDownAct[i]?.Invoke(pos);
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


#if UNITY_EDITOR
    void OnMouseEvent()
    {
        Vector3 pos = Input.mousePosition;
        int cnt = touchAct.Count;
        for (int i = 0; i < cnt; i++)
        {
            touchAct[i]?.Invoke(pos);
        }
    }

    private void OnMouseDragUpEvent()
    {
        Vector3 pos = Input.mousePosition;
        int upCnt = dragUpAct.Count;
        for (int i = 0; i < upCnt; i++)
        {
            dragUpAct[i]?.Invoke(pos);
        }
    }

    private void OnMouseDragDownEvent()
    {
        Vector3 pos = Input.mousePosition;
        int cnt = dragDownAct.Count;
        for (int i = 0; i < cnt; i++)
        {
            dragDownAct[i]?.Invoke(pos);
        }
    }
#endif
}
