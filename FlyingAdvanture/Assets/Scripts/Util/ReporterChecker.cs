using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReporterChecker : MonoBehaviour
{
    private void Start()
    {
#if REAL
        gameObject.SetActive(false);
#endif
    }
}
