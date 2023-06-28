using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] _mainTextArr;

    [SerializeField]
    Image startImg;

    Vector3[] vecArr;

    bool touchFlag = false;

    private void Awake()
    {
        touchFlag = false;
        GlobalData.gameState.gamePos = GameStateData.GamePos.INTROSCENE;
        GlobalData.gameState.gamePlayeState = GameStateData.GamePlayState.WAITING;

        ApplicationManager.Instance.OnStart();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitTxt();
        DoTweenStartTxt();
        InitStartImg();
    }

    void InitStartImg()
    {
        startImg.gameObject.SetActive(false);
        startImg.color = new Color(0, 0, 0, 0);
    }

    void InitTxt()
    {
        vecArr = new Vector3[_mainTextArr.Length];
        for (int i = 0; i < _mainTextArr.Length; i++)
        {
            _mainTextArr[i].color = new Color(0, 0, 0, 0);
            vecArr[i] = _mainTextArr[i].rectTransform.position;
        }
    }
    void DoTweenStartTxt()
    {
        Fade();
        MoveX();
        MoveY();
    }

    void Fade()
    {
        DOTween.Sequence().
            Append(_mainTextArr[0].DOColor(new Color(1f, 1f, 1f, 1f), 2f)).
            Append(_mainTextArr[1].DOColor(new Color(1f, 1f, 1f, 1f), 2f));
    }

    void MoveX()
    {
        float xpos1 = _mainTextArr[0].rectTransform.position.x;
        float xpos2 = _mainTextArr[1].rectTransform.position.x;

        DOTween.Sequence().
            Append(_mainTextArr[0].transform.DOMoveX(xpos1 + 10f, 0.01f)).
            Append(_mainTextArr[0].transform.DOMoveX(xpos1 - 10f, 0.01f)).
            SetLoops(100).OnComplete(() =>
            {
                DOTween.Sequence().
                    Append(_mainTextArr[1].transform.DOMoveX(xpos2 + 10f, 0.01f)).
                    Append(_mainTextArr[1].transform.DOMoveX(xpos2 - 10f, 0.01f)).
                    SetLoops(100).OnComplete(() =>
                    {
                        for (int i = 0; i < _mainTextArr.Length; i++)
                        {
                            _mainTextArr[i].rectTransform.position = vecArr[i];
                        }
                    });
            });
    }

    void MoveY()
    {
        float ypos1 = _mainTextArr[0].rectTransform.position.y;
        float ypos2 = _mainTextArr[1].rectTransform.position.y;

        DOTween.Sequence().
            Append(_mainTextArr[0].transform.DOMoveY(ypos1 + 1f, 0.01f)).
            Append(_mainTextArr[0].transform.DOMoveY(ypos1 - 1f, 0.01f)).
            SetLoops(100).OnComplete(() =>
            {
                DOTween.Sequence().
                    Append(_mainTextArr[1].transform.DOMoveX(ypos2 + 1f, 0.01f)).
                    Append(_mainTextArr[1].transform.DOMoveX(ypos2 - 1f, 0.01f)).
                    SetLoops(100).OnComplete(() =>
                    {
                        for (int i = 0; i < _mainTextArr.Length; i++)
                        {
                            _mainTextArr[i].rectTransform.position = vecArr[i];
                        }

                        WaitManager.Instance.StartWait(1f, () =>
                        {
                            EraseTxt();
                        });
                    });
            });
    }

    void EraseTxt()
    {
        DOTween.Sequence().
            Append(_mainTextArr[0].DOFade(0f, 1f)).
            Join(_mainTextArr[1].DOFade(0f, 1f)).
            OnComplete(() =>
            {
                PrintStartImage();
            });
    }

    void PrintStartImage()
    {
        startImg.gameObject.SetActive(true);
        startImg.DOColor(new Color(1f, 1f, 1f, 1f), 1f);

        UIEffectManager.BounceEffect(startImg.gameObject, 1.02f, 0.98f, 0.25f, (int)EffectAttribute.INFINITE);
    }

    public void OnClickStart()
    {
        if (touchFlag) return;
        touchFlag = true;

        UIEffectManager.TouchSizeUpErase(startImg.gameObject, 1.1f, 1f).OnComplete(() =>
        {
            startImg.gameObject.SetActive(false);
            LoadingUI.Instance.isActive = true;
         
            if(CheckConnectNetwork() == false)
                return;
            
            GoogleLogin.OnLogin(() =>
            {
                BackEndLogger.Log("LoginSeqeunce", BackEndLogger.LogType.NOMAL, "로그인 성공 뒤끝 로그인 시작");
                BackEndUser.Instance.LoginBackEnd((result) =>
                {
                    if (result == true)
                    {
                        Debug.Log("로그인 성공");
                        ThreadEvent.Instance.AddThreadEvent(() =>
                        {
                            BackEndLogger.Log("IntroSceneManager", BackEndLogger.LogType.NOMAL, "로비씬으로 이동");

                            GlobalData.IsGoogleLogin = true;
                            Debug.Log("Start Login Call Back");
                            LoadingUI.Instance.isActive = false;
                            LoadSceneManager.Instance.LoadScene(StringList.LobbyScene);
                            //SceneManager.LoadScene(StringList.LobbyScene);
                        });
                    }
                    else
                    {
                        Debug.Log("뒤끝 실패");
                        GlobalData.IsGoogleLogin = false;
                        GlobalData.Uid = null;
                        ShowQuitPopup(StringList.OffLineNotice);
                    }
                });
            },
            () =>
            {
                GlobalData.IsGoogleLogin = false;
                GlobalData.Uid = null;
                ShowQuitPopup(StringList.NotLoginGame);
            });
        });
    }

    public bool CheckConnectNetwork()
    {
        if (NetWorkManager.Instance.IsConnectNetWork == false)
        {
            ShowQuitPopup(StringList.OffLineNotice);
            return false;
        }
        return true;
    }

    void ShowQuitPopup(string message)
    {
        Debug.Log("Start ShowQuitPopup");

        ThreadEvent.Instance.AddThreadEvent(() =>
        {
            var popup = PopupComponent.PopupShow<NoticePopup>(PopupPath.PopupNotice);
            popup.SetOkAct(UtilManager.Quit);
            popup.SetCancleAct(UtilManager.Quit);
            WaitManager.Instance.StartWait(20f, () =>
            {
                UtilManager.Quit();
            });
        });
    }
}

