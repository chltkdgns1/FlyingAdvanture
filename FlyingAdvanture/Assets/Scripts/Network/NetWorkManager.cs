using UnityEngine;

public class NetWorkManager : MonoSingleTon<NetWorkManager>
{
    public static NetWorkManager instance;

    float disconnectTime;
    float disconnectLimitTime = 3f;

    bool disconnectMessage;
    bool IsDiconnect = false;

    protected override void Init()
    {
        ResetState();
    }

    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            disconnectTime += Time.deltaTime;
            PrintLinkLessMesssage();

            if (disconnectLimitTime <= disconnectTime)
            {
                Disconnect();
                return;
            }

            GlobalData.IsConnectNetWork = false;
        }
        else
        {
            ResetState();
            GlobalData.IsConnectNetWork = true;
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


    void PrintLinkLessMesssage()
    {
        if (disconnectMessage == false)
        {
            disconnectMessage = true;
            PopupComponent.PopupShow<NoticePopup>(PopupPath.PopupNotice);
        }
    }
}

