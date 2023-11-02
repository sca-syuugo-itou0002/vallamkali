using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using static SpeedEffectController;

public class SlotCont3 : MonoBehaviour
{ 
    [SerializeField] 
    private GameObject juge1;
    [SerializeField]
    private GameObject jugeFream;
    [SerializeField] 
    private GameObject juge2;
    [SerializeField]
    private GameObject juge2Fream;
    private float duration = 4.0f; // 縮小の期間（秒)
    public float targetx = 0.8f; // 最終的な幅
    public Vector3 StartPositionJuge = new Vector3(-1400, 0, 0);
    public Vector3 TargetPositionJuge=new Vector3(-189,0,0);
    public Vector3 StartPositionJuge02 = new Vector3(1400, 0, 0);
    public Vector3 TargetPositionJuge02 = new Vector3(0, 0, 0);
    public float targety = 0.8f; // 最終的な高さ
    public float resetDuration = 0.5f; // リセットの期間（秒）
    private Vector3 startScale=new Vector3(3.0f,3.0f,1);
    private Vector3 startScale2= new Vector3(3.0f, 3.0f, 1);
    private float startTime=0;
    private float startTime02=0;
    public Vector3 StoppedScaleLeft; // 停止時のサイズを記録(左)
    public Vector3 StoppedScaleRight; // 停止時のサイズを記録(右)
    private bool isScalingLeft = true;
    private bool isScalingRight = true;
    private float stopduration01=0f;
    private float stop01= 0f;
    private float stopduration02=0f;
    private float stop02 = 0f;
    private bool beingMeasured;

    [SerializeField] private float speed;

    private int combos = 0;
    public int Combos { set { combos = value; } }

    private bool isStopLeft = false;
    private bool isStopRight = false;

    [SerializeField] 
    private PlayerMoveTest pm;
    [SerializeField]
    private ScoreManagerTest sm;
    //UI
    [SerializeField] 
    private StateUI leftText;
    [SerializeField] 
    private StateUI1 rightText;

    [SerializeField] 
    private SpeedEffectController sec;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    AudioSource audioSource;
    bool[] isRight={ false,false};
    float[] touchduration = {0,0};
    Vector2[] startpos = new Vector2[2];
    public enum TIMING_STATE
    {
        Bad,
        Good,
        Great
    }
    private void Start()
    {
        pm = FindObjectOfType<PlayerMoveTest>();
        audioSource = GetComponent<AudioSource>();
        sm=FindObjectOfType<ScoreManagerTest>();
        JugeMove();
        
    }
    void Awake()
    {
        juge1.SetActive(false);
        jugeFream.SetActive(false);
        juge2.SetActive(false);
        juge2Fream.SetActive(false);
        //JugeMove();
    }
    private void Update()
    {
        touchduration[0]+=Time.deltaTime;
        touchduration[1]+=Time.deltaTime;
        var touchcount=Input.touchCount;
        for (int i = 0; i < touchcount; i++)
        {
            var touch=Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("isTouch" + touch.fingerId);
                    touchduration[touch.fingerId]=0;
                    startpos[touch.fingerId]=touch.position;
                    if (touch.position.x > 720)
                    {
                        isRight[touch.fingerId]=true;
                        //Juge2Move();
                    }
                    else
                    {
                        isRight[touch.fingerId]=false;
                        //JugeMove();
                    }
                    break;
                case TouchPhase.Moved:
                    
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    if (touchduration[touch.fingerId] > 0.3f) return;
                    Vector2 endpos = touch.position;
                    float diffY = endpos.y - startpos[touch.fingerId].y;
                    if (diffY > -200) return;
                    if (isRight[touch.fingerId] == true)
                    {
                        //Debug.Log("RightSwaip");
                        RightButtonClicked();
                    }
                    else
                    {
                        //Debug.Log("LeftSwaip");
                        LeftButtonClicked();

                    }
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }
    }
    private void JugeMove()
    {
        if (juge1 != null && juge2 != null)
        {
            juge1.SetActive(true);
            jugeFream.SetActive(true);
            juge1.transform.localScale = startScale;
            startTime = Time.time;
            StartCoroutine(ScaleObjectOverTimeJuge1());

        }
        else
        {
            //Debug.LogError("Target GameObject is not assigned.");
        }
    }
    private void Juge2Move()
    {
        //Debug.Log("Yes");
        if (juge1 != null && juge2 != null)
        {
            new WaitForSeconds(2.0f);
            juge2.SetActive(true);
            juge2Fream.SetActive(true);
            juge2.transform.localScale = startScale2;
            startTime02 = Time.time;
            StartCoroutine(ScaleObjectOverTimeJuge2());

        }
        else
        {
            //Debug.LogError("Target GameObject is not assigned.");
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
            jugeFream.SetActive(false);
            leftText.StateDisplay(CheckScale());
            Juge2Move();
        }
        stop01=stopduration01;
        stopduration01=0;
        float jyoukenn1 = 8 % stop01;
        Debug.Log(stop01);
        StopJudge();
    }

    public void RightButtonClicked()
    {
            isStopRight=true;
        if (isScalingRight)
        {
            StoppedScaleRight=juge2.transform.localScale;
            juge2.SetActive(false);
            juge2Fream.SetActive(false);
            rightText.StateDisplay(CheckScale());
        }
        stop02 = stopduration02;
        stopduration02=0;
        Debug.Log(stop02);
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
    private IEnumerator ScaleObjectOverTimeJuge1()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newX = Mathf.Lerp(startScale.x, targetx, t);
            float newY = Mathf.Lerp(startScale.y, targety, t);
            float targetX = Mathf.Lerp(StartPositionJuge.x, TargetPositionJuge.x, t);
            //Debug.Log(targetX);
            juge1.GetComponent<RectTransform>().anchoredPosition = new Vector3(targetX, -80, 1.0f);
            juge1.transform.localScale = new Vector3(newX, newY, 1.0f);
            elapsedTime = Time.time - startTime;
            stopduration01+=Time.deltaTime;
            //Debug.Log(elapsedTime);
            yield return null;
        }
        juge1.transform.localScale = new Vector3(targetx, targety, 1.0f);
    }
    private IEnumerator ScaleObjectOverTimeJuge2()
    {
        float elapsedTime02 = 0f;

        while (elapsedTime02 < duration)
        {
            float t = elapsedTime02 / duration;
            float newX = Mathf.Lerp(startScale2.x, targetx, t);
            float newY = Mathf.Lerp(startScale2.y, targety, t);
            float targetX=Mathf.Lerp(StartPositionJuge02.x, TargetPositionJuge02.x,t);
            juge2.GetComponent<RectTransform>().anchoredPosition = new Vector3(targetX, -80, 1.0f);
            juge2.transform.localScale = new Vector3(newX, newY, 1.0f);
            elapsedTime02 = Time.time - startTime02;
            stopduration02 += Time.deltaTime;
           yield return null;
        }
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
        JugeMove();
    }
    private  TIMING_STATE CheckScale()
    {

        float jyoukenn1 = duration / stop01;
        //Debug.Log(duration);
        Debug.Log($"jyouken1,{jyoukenn1}");
        float jyoukenn2 = duration / stop02;
        //Debug.Log(duration);
        //Debug.Log($"jyouken2,{jyoukenn2}");
        if (jyoukenn1 >= 0 && jyoukenn1 <= 1 || jyoukenn2 >= 0  && jyoukenn2 <= 1) 
        {
            sec.AddSpeed();
            audioSource.PlayOneShot(sound1);
            //Debug.Log("1");
            return TIMING_STATE.Great;
           
        }
        if (jyoukenn1 > 1.1 && jyoukenn1 <= 2 || jyoukenn2 > 1.1 && jyoukenn2 <= 2)
        {
            audioSource.PlayOneShot(sound2);
            ///Debug.Log("2");
            return TIMING_STATE.Good;

        }
        else
        {
            sec.SubSpeed();
            audioSource.PlayOneShot(sound3);
            //Debug.Log("3");
            return TIMING_STATE.Bad;
        }
     }
}
