using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // プレイヤーのTransformを設定
    public Vector3 offset; // カメラの位置オフセット

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
