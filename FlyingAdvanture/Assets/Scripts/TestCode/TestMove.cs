using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(new Vector3(10, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
