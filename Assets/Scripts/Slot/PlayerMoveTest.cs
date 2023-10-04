using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField]private float speed = 0.05f; // プレイヤーの移動速度
    private float targetY; // 移動のターゲット位置
    private float startY; // 移動開始時のY座標
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
            float currentTime = Time.deltaTime;
            if (currentTime - lastMoveTime >= moveInterval)
            {
                targetY = transform.position.y + 3.0f; // 1.0f の距離だけ上に移動
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
        targetY = startY + 3.0f; // 上方向に2.0f移動
        StartCoroutine(Player());
        
       
    }
    IEnumerator Player()
    {
        // ターゲット位置まで移動
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
