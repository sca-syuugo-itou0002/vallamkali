using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // �v���C���[�̈ړ����x
    private bool isMoving = false; // �v���C���[���ړ������ǂ����̃t���O
    private float targetY; // �ړ��̃^�[�Q�b�g�ʒu
    private float startY; // �ړ��J�n����Y���W
    private float moveInterval = 1.0f; // �ړ��Ԋu
    private float lastMoveTime; // �Ō�Ɉړ���������
    public Text Scoretext;
    private int score_num=0;

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
            if (Input.GetMouseButtonUp(0))
            {
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
            score_num+=1;
            Destroy(other.gameObject);
        }
    }
    public void SwitchResultScene()
    {
        
    }
}
