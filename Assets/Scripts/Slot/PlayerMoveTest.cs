using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField]private float speed = 0.05f; // �v���C���[�̈ړ����x
    private float targetY; // �ړ��̃^�[�Q�b�g�ʒu
    private float startY; // �ړ��J�n����Y���W
    GameObject Maingame_BG;
    [SerializeField] private Enemy en;
#if false
   void Awake()
    {
        startY = transform.position.y;
        lastMoveTime = Time.time;
    }
    public void PlayerMove()
    {
        startY = transform.position.y;
        lastMoveTime = Time.deltaTime;
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
            float currentTime = Time.deltaTime;
            if (currentTime - lastMoveTime >= moveInterval)
            {
                targetY = transform.position.y + 3.0f; // 1.0f �̋���������Ɉړ�
                isMoving = true;
                lastMoveTime = currentTime;
            }
        }
    }
#endif
    void Start()
    {
        startY = transform.position.y;
        en = FindObjectOfType<Enemy>();
    }

    public void PlayerMove()
    {
        Debug.Log("Yes");

        // = transform.position.y;
        targetY = startY + 3.0f; // �������2.0f�ړ�
        StartCoroutine(Player());
        
       
    }
    IEnumerator Player()
    {
        // �^�[�Q�b�g�ʒu�܂ňړ�
        float step = 0f;
        Vector3 startPos = transform.position;
        while (step < 1)
        {
            transform.position = Vector3.Lerp(startPos,
            new Vector3(transform.position.x, targetY, transform.position.z), step);
            step += speed *Time.deltaTime;
            yield return null;
        }
        step = 0;
        startPos = transform.position;
        while (step < 1)
        {
            transform.position = Vector3.Lerp(startPos,
            new Vector3(transform.position.x, startY, transform.position.z), step);
            step += speed * Time.deltaTime;
            yield return null;
        }
    }
}
