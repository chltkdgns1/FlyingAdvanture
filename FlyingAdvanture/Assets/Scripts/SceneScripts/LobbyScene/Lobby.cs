using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    private void Awake()
    {
        ConnectPhoton();
    }
    public void ConnectPhoton()
    {
        if (NetWorkManager.instance.IsConnectNetWork == false)
            return;

        CustomPhoton.ConnectPhoton.Instance.IsWantConnect = true;
    }

    public void MatchingGame()
    {
        CustomPhoton.Matching.Instance.JoinRoom();
    }
}
