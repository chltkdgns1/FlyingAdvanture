using UnityEngine;
using UnityEngine.UI;

public partial class UILobbyScene
{
    public void SetResetStartBtn()
    {
        if (startBtn == null)
        {
            BackEndLogger.Log("Error", BackEndLogger.LogType.ERROR, "SetResetStartBtn startBtn == null");
            return;
        }

        if (startBtn.gameObject == null)
        {
            BackEndLogger.Log("Error", BackEndLogger.LogType.ERROR, "SetResetStartBtn startBtn.gameObject == null");
            return;
        }

        startBtn.gameObject.transform.localScale = new Vector3(1, 1);
        startBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void SetPrintStartBtn()
    {
        if (startBtn == null)
        {
            BackEndLogger.Log("Error", BackEndLogger.LogType.ERROR, "SetPrintStartBtn startBtn == null");
            return;
        }

        startBtn.SetActive(true);
    }

    public void SetEraseStartBtn()
    {
        if (startBtn == null)
        {
            BackEndLogger.Log("Error", BackEndLogger.LogType.ERROR, "SetEraseStartBtn startBtn == null");
            return;
        }

        startBtn.SetActive(false);
    }
 
    public void SetPrintMatchPopupReady()
    {
        if (GlobalData.IsOpenRankingChallenge)
        {
            PopupComponent.PopupShow<NoticePopup>(PopupPath.PopupRanking);
        }
        else
        {
            PopupComponent.PopupShow<NoticePopup>(PopupPath.PopupNotice);
        }
    }
}
