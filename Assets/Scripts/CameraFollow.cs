using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // �v���C���[��Transform��ݒ�
    public Vector3 offset; // �J�����̈ʒu�I�t�Z�b�g

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
