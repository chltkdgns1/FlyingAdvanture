using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadEvent : MonoSingleTon<ThreadEvent>
{
    public delegate void EventParam(params object[] ob);

    class EventParamData
    {
        public object[] param;
        public EventParam evt;

        public EventParamData(EventParam evt, object[] param)
        {
            this.evt = evt;
            this.param = param;
        }
    }

    Queue<Action> evtQueue = new Queue<Action>();
    Queue<EventParamData> evtParamQueue = new Queue<EventParamData>();

    object evtQueueLock = new object();
    object evtParamQueueLock = new object();

    protected override void Init() { }

    void Update()
    {
        ExcuteEvtQueue();
        ExcuteEvtParamQueue();
    }

    void ExcuteEvtQueue()
    {
        if (evtQueue.Count == 0)
            return;

        Action actTemp = null;
        lock (evtQueueLock)
        {
            actTemp = evtQueue.Dequeue();
        }
        actTemp?.Invoke();
    }

    void ExcuteEvtParamQueue()
    {
        if (evtParamQueue.Count == 0)
            return;

        EventParamData actTemp = null;
        lock (evtParamQueueLock)
        {
            actTemp = evtParamQueue.Dequeue();
        }
        actTemp.evt?.Invoke(actTemp.param);
    }

    public void AddThreadEvent(Action act)
    {
        lock (evtQueueLock)
        {
            evtQueue.Enqueue(act);
        }
    }

    public void AddThreadEventParam(EventParam evt, params object[] ob)
    {
        lock (evtParamQueueLock)
        {
            evtParamQueue.Enqueue(new EventParamData(evt, ob));
        }
    }

    public void OnStart() { }
}
