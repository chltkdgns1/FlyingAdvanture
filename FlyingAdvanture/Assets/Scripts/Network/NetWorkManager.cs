using UnityEngine;

public class NetWorkManager : MonoSingleTon<NetWorkManager>
{
    bool IsDiconnect = false;
    public bool IsConnectNetWork = false;

    protected override void Init()
    {

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

    public void OnStart() { }
}

