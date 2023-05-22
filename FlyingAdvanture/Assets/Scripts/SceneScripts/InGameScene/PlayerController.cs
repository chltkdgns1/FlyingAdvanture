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
        player = GetComponent<Player>();
        actionList.Add(new Idle(player));
        actionList.Add(new Move(player));
        actionList.Add(new Collisions(player));

        actionList[(int)actionState.MOVE].OnFinish(OnFinishAction);
        player.ActionData = actionList[(int)actionState.IDLE];
    }

    private void Start()
    {
        if (isMine)
        {
            TouchScreen.Instance.AddEvent(OnTCEvent);
            RotateScreen.Instance.AddEvent(OnACEvent);
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

    public void OnTCEvent(Vector3 pos)
    {
        // 충돌 상태에서는 움직일 수 없음. 튕겨나가는 중이기 때문에
        if (state == actionState.COLLISION)
            return;

        state = actionState.MOVE;
        int index = (int)actionState.MOVE;
        player.ActionData = actionList[index];
        actionList[index].OnTarget(pos);
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
