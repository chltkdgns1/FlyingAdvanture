using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{ 
    public Player player;
    public Vector3 cameraPos;
    public Vector3 rotation;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.TransformPoint(cameraPos), Time.deltaTime * 10f);
        // 카메라가 바라볼 대상을 정해준다.
        transform.rotation = player.transform.rotation * Quaternion.Euler(rotation);
    }
}