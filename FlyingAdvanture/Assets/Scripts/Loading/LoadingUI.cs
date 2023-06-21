using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoSingleTon<LoadingUI>
{
    [System.NonSerialized]
    public bool isActive;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if (isActive)
            {
                gameObject.SetActive(true);
                gameObject.transform.SetAsLastSibling();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected override void Init()
    {
        gameObject.SetActive(false);
    }
}