using DG.Tweening;
using UnityEngine;

public partial class UILobbyScene : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.gameState.gamePos = GameStateData.GamePos.LOBBYSCENE;
        GlobalData.gameState.gamePlayeState = GameStateData.GamePlayState.WAITING;
    }

    private void Start()
    {
        Init();
    }

    public void OnClickStartBtn()
    {
        //SetPrintGameLevelPopup();
    }

    public void OnClickSetting()
    {
        //SetPrintSettingBack();
    }
}
