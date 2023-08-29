using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // �v���C���[�̈ړ����x
    private bool isMoving = false; // �v���C���[���ړ������ǂ����̃t���O
    private float targetY; // �ړ��̃^�[�Q�b�g�ʒu
    private float startY; // �ړ��J�n����Y���W
    private float moveInterval = 3.0f; // �ړ��Ԋu
    private float lastMoveTime; // �Ō�Ɉړ���������
    //private Rigidbody2D rb;
    private void Start()
    {
        startY = transform.position.y;
        lastMoveTime = Time.time;
    }
    void Update()
    {
        if (isMoving)
        {
            // �^�[�Q�b�g�ʒu�܂ňړ�
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), step);

            // �ڕW�ʒu�ɓ��B�������ǂ������`�F�b�N
            if (Mathf.Approximately(transform.position.y, targetY))
            {
                isMoving = false;
            }
        }
        else
        {
            // �v���C���[��Y�������̈ړ����x��ݒ�
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(0f, verticalInput * speed * Time.deltaTime, 0f);

            // �v���C���[���ړ�������
            transform.Translate(movement);

            // �Q�[�W�̒l�Ɋ�Â��Ĉړ����J�n
            if (Input.GetMouseButtonDown(1))
            {
                /*
                float powerValue = FindObjectOfType<SliderMove>().GetPowerMeterValue();
                float distance = Mathf.Lerp(0, 3, powerValue);
                //float distance = 3.0f; // ��ɌŒ�̋�����i��
                targetY = startY + distance;
                //targetY = startY + distance * Mathf.Sign(verticalInput); // �v���C���[�̓��͂Ɋ�Â��Đi�s������ݒ�
                isMoving = true;*/
                float currentTime = Time.time;
                if (currentTime - lastMoveTime >= moveInterval)
                {
                    targetY = transform.position.y + 3.0f; // 1.0f �̋���������Ɉړ�
                    isMoving = true;
                    lastMoveTime = currentTime;
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
