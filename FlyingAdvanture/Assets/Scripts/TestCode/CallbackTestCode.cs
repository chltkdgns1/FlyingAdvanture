using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackTestCode : MonoBehaviour
{
    int number = 3;

    // Start is called before the first frame update
    void Start()
    {
        CallbackTestCodeSingleTon.Instance.act += () =>
        {
            int number = 3;
            Debug.Log("number : " + number);
        };

        CallbackTestCodeSingleTon.Instance.WaitStart();
        StartCoroutine(CoWaitChanger());
    }

    IEnumerator CoWaitChanger()
    {
        yield return new WaitForSeconds(3f);
        number = 5;
    }
}
