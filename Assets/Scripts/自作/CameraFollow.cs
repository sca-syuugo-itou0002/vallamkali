using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTr; // プレイヤーのTransform
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 4.0f, -10f); // カメラの初期位置位置 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(5, 15); // カメラの右上限界座標
    [SerializeField] Vector2 camaraMinPos = new Vector2(-5, -15); // カメラの左下限界座標

    void LateUpdate()
    {

        Vector3 playerPos = playerTr.position; // プレイヤーの位置
        Vector3 camPos = transform.position; // カメラの位置

        // 滑らかにプレイヤーの場所に追従
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // カメラの位置を制限
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -10f;
        if (camPos.y == 15.0f)
        {
            camPos=cameraOrgPos;
        }
        transform.position = camPos;

    }
}
