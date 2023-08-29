using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        // Player�̕����̓J�������ǂ����������I�u�W�F�N�g�̖��O�������
        this.player = GameObject.Find("Player");
    }
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            transform.position.x, playerPos.y, transform.position.z);
    }
}
