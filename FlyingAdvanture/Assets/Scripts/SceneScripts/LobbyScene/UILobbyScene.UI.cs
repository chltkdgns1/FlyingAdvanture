using UnityEngine;

public partial class UILobbyScene
{
    #region 시작 버튼
    [SerializeField]
    GameObject startBtn;
    #endregion

    public void Init()
    {
      
    }

    public void ResetScreen()
    {
        SetEraseGameLevelPopup();
        SetResetStartBtn();
    }
}
