using UnityEngine;

public class NetWorkManager : MonoSingleTon<NetWorkManager>
{
    public static NetWorkManager instance;

    float disconnectTime;
    float disconnectLimitTime = 3f;

    bool disconnectMessage;
    bool IsDiconnect = false;

    public bool IsConnectNetWork = false;

    protected override void Init()
    {
        ResetState();
    }

    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            IsConnectNetWork = false;
            if (LoadingUI.Instance.isActive == false)
                LoadingUI.Instance.isActive = true;
        }
        else
        {
            ResetState();
            IsConnectNetWork = true;
            if (LoadingUI.Instance.isActive == true)
                LoadingUI.Instance.isActive = false;
        }
    }

    public void Disconnect()
    {
        if (IsDiconnect == true)
            return;

        IsDiconnect = true;
        GlobalData.IsGoogleLogin = false;
        PopupComponent.PopupShow<NoticePopup>(PopupPath.PopupNotice);
        Invoke("Quit", 3f);
    }

    void Quit()
    {
        UtilManager.Quit();
    }

    void ResetState()
    {
        disconnectMessage = false;
        disconnectTime = 0f;
    }
}

