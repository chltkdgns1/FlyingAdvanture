using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayignGameManager : MonoSingleTon<PlayignGameManager>
{
    public bool IsStop { get; set; } = false;
    public bool IsOnline { get; set; } = false;

    protected override void Init()
    {
        
    }
}
