using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using static SpeedEffectController;

public class SlotCont3 : MonoBehaviour
{ 
    [SerializeField] private GameObject juge1;
    [SerializeField] private GameObject juge2;
    public float duration = 5.0f; // 縮小の期間（秒）
    public float targetx = 0f; // 最終的な幅
    public float targety = 0f; // 最終的な高さ
    public float resetDuration = 2.0f; // リセットの期間（秒）
    private Vector3 startScale;
    private float startTime;
    public Vector3 StoppedScaleLeft; // 停止時のサイズを記録(左)
    public Vector3 StoppedScaleRight; // 停止時のサイズを記録(右)
    private bool isScalingLeft = true;
    private bool isScalingRight = true;
    Vector3 GreatPoint = new Vector3(0.75f, 0.75f, 1);
    Vector3 GoodPoint = new Vector3(0.5f, 0.5f, 1);

    [SerializeField] private float speed;

    private int combos = 0;
    public int Combos { set { combos = value; } }

    private bool isStopLeft = false;
    private bool isStopRight = false;

    [SerializeField] private PlayerMoveTest pm;
    //UI
    [SerializeField] private StateUI leftText;
    [SerializeField] private StateUI rightText;

    [SerializeField] private SpeedEffectController sec;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;

    public enum TIMING_STATE
    {
        Bad,
        Good,
        Great
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    void Awake()
    {
        pm = FindObjectOfType<PlayerMoveTest>();
        new WaitForSeconds(2.0f);
        juge1.SetActive(false);
        juge2.SetActive(false);
        JugeMove();
       
    }
    private void JugeMove()
    {
        if (juge1 != null && juge2 != null)
        {
            juge1.SetActive(true);
            juge2.SetActive(true);
            startScale = juge1.transform.localScale;
            startScale = juge2.transform.localScale;
            startTime = Time.time;
            StartCoroutine(ScaleObjectOverTime());
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }
    }
    // マウスのクリック操作をボタンに関連付けるための関数
    public void LeftButtonClicked()
    {
            isStopLeft=true;
        if (isScalingLeft)
        {
            StoppedScaleLeft=juge1.transform.localScale;
            juge1.SetActive(false);
            leftText.StateDisplay(CheckScale());
            Debug.Log("juge1Scale");
            Debug.Log(juge1.transform.localScale);
        }
            StopJudge();
    }

    public void RightButtonClicked()
    {
            isStopRight=true;
        if (isScalingRight)
        {
            StoppedScaleRight=juge2.transform.localScale;
            juge2.SetActive(false);
            rightText.StateDisplay(CheckScale());
            Debug.Log("juge2Scale");
            Debug.Log(juge2.transform.localScale);
        }
            StopJudge();
    }
    void StopJudge()
    {
        if (isStopLeft == true && isStopRight == true)
        {
            StartCoroutine(ResetButton());
            if (pm != null)
            {
                pm.PlayerMove();
            }
        }
    }
    private IEnumerator ScaleObjectOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (isScalingLeft==isScalingRight)
            {
                float t = elapsedTime / duration;
                float newX = Mathf.Lerp(startScale.x, targetx, t);
                float newY = Mathf.Lerp(startScale.y, targety, t);
                juge1.transform.localScale = new Vector3(newX, newY, 1.0f);
                juge2.transform.localScale = new Vector3(newX, newY, 1.0f);
                elapsedTime = Time.time - startTime;
                yield return null;
            }
            else
            {
                yield return null;
            }
        }

        // 確実に最終的なサイズを設定
        juge1.transform.localScale = new Vector3(targetx, targety, 1.0f);
        juge2.transform.localScale = new Vector3(targetx, targety, 1.0f);
    }
    private IEnumerator ResetButton()
    {
        isStopLeft = false;
        isStopRight = false;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ResetScale());
    }


    IEnumerator ResetScale()
    {
        if (!isScalingLeft&&!isScalingRight)
        {
            // リセット中なので何もしない
            yield break;
        }

        isScalingLeft = false;
        isScalingRight=false;

        // サイズをリセット
        Vector3 currentScaleLeft = juge1.transform.localScale;
        Vector3 currentScaleRight = juge2.transform.localScale;
        float resetStartTime = Time.time;
        float resetElapsedTime = 0f;

        while (resetElapsedTime < resetDuration)
        {
            float t = resetElapsedTime / resetDuration;
            float newXLeft = Mathf.Lerp(currentScaleLeft.x, startScale.x, t);
            float newYLeft = Mathf.Lerp(currentScaleLeft.y, startScale.y, t);
            float newXRight = Mathf.Lerp(currentScaleRight.x, startScale.x, t);
            float newYRight = Mathf.Lerp(currentScaleRight.y, startScale.y, t);
            juge1.transform.localScale = new Vector3(newXLeft, newYLeft, 1.0f);
            juge2.transform.localScale = new Vector3(newXRight, newYRight, 1.0f);
            resetElapsedTime = Time.time - resetStartTime;
            yield return null;
        }

        // リセット後、再び縮小を開始
        isScalingLeft = true;
        juge1.SetActive(true);
        isScalingRight=true;
        juge2.SetActive(true);
        startTime = Time.time;
        StartCoroutine(ScaleObjectOverTime());
    }
    private  TIMING_STATE CheckScale()
    {
        Vector3 ScaleLeft = StoppedScaleLeft;
        Vector3 ScaleRight = StoppedScaleRight;
        if (ScaleLeft==GreatPoint||ScaleRight==GreatPoint) 
        {
            sec.AddSpeed();
            audioSource.PlayOneShot(sound1);
            return TIMING_STATE.Great;
           
        }
        
        
        
        if (ScaleLeft==GoodPoint||ScaleRight==GoodPoint)
        {
            audioSource.PlayOneShot(sound2);
            return TIMING_STATE.Good;

        }
        else
        {
            sec.SubSpeed();
            audioSource.PlayOneShot(sound3);
            return TIMING_STATE.Bad;
        }

     }
}
