using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScene : MonoBehaviour
{
    IGameLogic gameLogic;

    [SerializeField]
    public Text startWaitTxt; 

    private void Awake()
    {
        gameLogic = GlobalData.gameLogicData;
        gameLogic.SetInGameScene(this);
        PlayignGameManager.Instance.IsStop = true;

        InitUI();
        InitGame();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        startWaitTxt.gameObject.SetActive(true);
        gameLogic.WaitGameStart(()=>
        {
            startWaitTxt.gameObject.SetActive(false);
            PlayignGameManager.Instance.IsStop = false;
        });
    }

    public void InitUI()
    {

    }

    public void InitGame()
    {

    }
}
