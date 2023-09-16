using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTr; // �v���C���[��Transform
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 4.0f, -10f); // �J�����̏����ʒu�ʒu 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(5, 15); // �J�����̉E����E���W
    [SerializeField] Vector2 camaraMinPos = new Vector2(-5, -15); // �J�����̍������E���W

    void LateUpdate()
    {

        Vector3 playerPos = playerTr.position; // �v���C���[�̈ʒu
        Vector3 camPos = transform.position; // �J�����̈ʒu

        // ���炩�Ƀv���C���[�̏ꏊ�ɒǏ]
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f * Time.deltaTime);

        // �J�����̈ʒu�𐧌�
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
