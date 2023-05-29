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
    }

    public void StartGame()
    {
        startWaitTxt.gameObject.SetActive(true);
        gameLogic.WaitGameStart(()=>
        {
            startWaitTxt.gameObject.SetActive(false);
            InitUI();
            InitGame();
        });
    }

    public void InitUI()
    {

    }

    public void InitGame()
    {

    }
}
