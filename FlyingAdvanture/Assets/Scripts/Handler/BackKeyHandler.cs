using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BackKeyHandler 는 
public class BackKeyHandler : MonoBehaviour
{
    // awake 를 상속받아 사용못하니 DoAwake 를 상속받아 처리.
    public virtual void DoAwake()
    {

    }

    // DoDisable 도 마찬가지
    public virtual void DoDestroy()
    {

    }

    private void Awake()
    {
        BackKeyManager.Instance.Add(this);
        DoAwake();
    }

    private void OnDestroy()
    {
        DoDestroy();
    }

    public virtual void OnClose()
    {
        BackKeyManager.Instance.Delete(this);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetLastSibiling()
    {
        BackKeyManager.Instance.SetLastSibling(this);
    }
}
