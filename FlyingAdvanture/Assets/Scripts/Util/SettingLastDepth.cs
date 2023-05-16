using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLastDepth : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(SetLastDepth());
    }

    IEnumerator SetLastDepth()
    {
        var waitor = new WaitForSeconds(0.1f); 
        while (true)
        {
            yield return waitor;
            transform.SetAsLastSibling();
        }
    }
}
