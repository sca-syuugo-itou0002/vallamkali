using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // プレイヤーの移動速度
    private bool isMoving = false; // プレイヤーが移動中かどうかのフラグ
    private float targetY; // 移動のターゲット位置
    private float startY; // 移動開始時のY座標
    private float moveInterval = 1.0f; // 移動間隔
    private float lastMoveTime; // 最後に移動した時間
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
            // ターゲット位置まで移動
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), step);

            // 目標位置に到達したかどうかをチェック
            if (Mathf.Approximately(transform.position.y, targetY))
            {
                isMoving = false;
            }
        }
        else
        {
            // プレイヤーのY軸方向の移動速度を設定
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(0f, verticalInput * speed * Time.deltaTime, 0f);

            // プレイヤーを移動させる
            transform.Translate(movement);

            // ゲージの値に基づいて移動を開始
            if (Input.GetMouseButtonUp(0))
            {
                float currentTime = Time.time;
                if (currentTime - lastMoveTime >= moveInterval)
                {
                    targetY = transform.position.y + 3.0f; // 1.0f の距離だけ上に移動
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
