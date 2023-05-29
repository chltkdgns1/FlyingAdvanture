using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;

    public bool isMine;
    List<IAction> actionList = new List<IAction>();

    public enum actionState
    {
        IDLE = 0,
        MOVE,
        COLLISION
    }

    actionState state;

    private void Awake()
    {

    }

    private void Start()
    {
        player = GetComponent<Player>();
        actionList.Add(new Idle(player));
        actionList.Add(new Move(player));
        actionList.Add(new Collisions(player));

        actionList[(int)actionState.MOVE].OnFinish(OnFinishAction);
        player.ActionData = actionList[(int)actionState.IDLE];

        if (isMine)
        {
#if UNITY_EDITOR
            TouchScreen.Instance.AddDragDownEvent(OnDragDownEvent);     // 드레그
            TouchScreen.Instance.AddDragUpEvent(OnDragUpEvent);
            KeyBoardManager.Instance.AddEvent(OnKeyBoardEvent);
#else
            TouchScreen.Instance.AddDragEvent(OnDragEvent); // 드레그
            TouchScreen.Instance.AddDragUpEvent(OnDragUpEvent);
            RotateScreen.Instance.AddEvent(OnACEvent);    // 기울이기
#endif
        }
    }

    public void OnACEvent(Vector3 accel)
    {
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
    }

#if UNITY_EDITOR
    public void OnClickEvent(Vector3 pos)
    {
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
        player.ActionData.OnTarget(pos);
    }

    public void OnKeyBoardEvent(KeyCode code)
    {
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
        player.ActionData.OnKeyBoardArrow(code);
    }

#endif

    public void OnDragDownEvent(Vector3 pos)
    {
        // 충돌 상태에서는 움직일 수 없음. 튕겨나가는 중이기 때문에
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
        actionList[index].OnRotationStart(pos);
    }

    public void OnDragUpEvent(Vector3 pos)
    {
        // 충돌 상태에서는 움직일 수 없음. 튕겨나가는 중이기 때문에
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
        actionList[index].OnRotationFinish(pos);
    }

    public void OnFinishAction()
    {
        state = actionState.IDLE;
        int index = (int)actionState.IDLE;
        player.ActionData = actionList[index];
    }

    private void OnCollisionEnter(Collision collision)
    {
        state = actionState.COLLISION;
        int index = (int)actionState.COLLISION;
        player.ActionData = actionList[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
