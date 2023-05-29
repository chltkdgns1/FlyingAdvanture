using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardManager : MonoSingleTon<KeyBoardManager>
{
    List<Action<KeyCode>> keyEventList = new List<Action<KeyCode>>();

    protected override void Init()
    {

    }

    public void AddEvent(Action<KeyCode> act)
    {
        keyEventList.Add(act);
    }

    public void DeleteEvent(Action<KeyCode> act)
    {
        keyEventList.Remove(act);
    }

    public void OnEvent(KeyCode key)
    {
        int cnt = keyEventList.Count;
        for (int i = 0; i < cnt; i++)
        {
            keyEventList[i]?.Invoke(key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            OnEvent(KeyCode.RightArrow);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnEvent(KeyCode.UpArrow);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnEvent(KeyCode.LeftArrow);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            OnEvent(KeyCode.DownArrow);
        }
    }
}
