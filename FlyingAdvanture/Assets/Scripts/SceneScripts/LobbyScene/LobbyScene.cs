using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene instance;

    public UILobbyScene UILobby { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else enabled = false;

        UILobby = GetComponent<UILobbyScene>();
        ConnectPhoton();
    }
    public void ConnectPhoton()
    {
        if (NetWorkManager.Instance.IsConnectNetWork == false)
            return;

        CustomPhoton.ConnectPhoton.Instance.IsWantConnect = true;
    }

    public void MatchingGame()
    {
        CustomPhoton.Matching.Instance.JoinRoom();
    }
}
