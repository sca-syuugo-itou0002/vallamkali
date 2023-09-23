using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using static SpeedEffectController;

public class SlotCont2 : MonoBehaviour
{
    [SerializeField] private Image leftPoint;
    [SerializeField] private Image leftBar;
    [SerializeField] private Image rightPoint;
    [SerializeField] private Image rightBar;
    [SerializeField] private Image leftCritical;
    [SerializeField] private Image rightCritical;

    [SerializeField] private float minBar;
    [SerializeField] private float maxBar;
    [SerializeField] private float minPoint;
    [SerializeField] private float maxPoint;
    [SerializeField] private float pointRange;
    [SerializeField] private float criticalPoint;
    
    private float leftStep;
    private float rightStep;

    [SerializeField] private float speed;

    private int combos = 0;
    public int Combos { set { combos = value; } }

    private bool isLeftStart = false;
    private bool isRightStart = false;
    
    private bool isStopLeft = false;
    private bool isStopRight = false;
    //private bool canStop = false;

    private int leftButtonClickCount = 0;
    private int rightButtonClickCount = 0;
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
        Initialization();
        pm = FindObjectOfType<PlayerMoveTest>();
       
    }
    // マウスのクリック操作をボタンに関連付けるための関数
    public void LeftButtonClicked()
    {
        Debug.Log("Left button clicked");
        if (leftButtonClickCount % 2 == 0)
        {
            
            isLeftStart = true;
        }
        else
        {
            isLeftStart = false;
            leftText.StateDisplay(CheckPosition(leftPoint, leftCritical, leftBar));
            isStopLeft=true;
            StopJudge();
        }
        leftButtonClickCount++;
    }

    public void RightButtonClicked()
    {
        Debug.Log("Right button clicked");
        if (rightButtonClickCount % 2 == 0)
        {
           
            isRightStart = true;
        }
        else
        {
            isRightStart = false;
            rightText.StateDisplay(CheckPosition(rightPoint, rightCritical, rightBar));
            isStopRight=true;
            StopJudge();
        }
        rightButtonClickCount++;
    }
    void StopJudge()
    {
        if (isStopLeft == true && isStopRight == true)
        {
            StartCoroutine(ResetButton());
            if (pm != null)
            {
                Debug.Log("20");
                pm.PlayerMove();
            }
        }
    }
    void Update()
    {
        if (isLeftStart)
        {
            leftStep = SetBar(leftBar, leftStep);
        }
        if (isRightStart)
        {
            rightStep = SetBar(rightBar, rightStep);
        }
        
#if false
        canStop = isLeftStart && isRightStart;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isLeftStart && !isStop)
        {
            isLeftStart = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isRightStart && !isStop)
        {   
            isRightStart = true;
        }
        if (Input.GetMouseButtonUp(0) && canStop)
        {
            leftText.StateDisplay(CheckPosition(leftPoint, leftCritical, leftBar));
            rightText.StateDisplay(CheckPosition(rightPoint, rightCritical, rightBar));
            isStop = true;
            StartCoroutine(ResetButton());
        }

        if (isLeftStart)
        {
            leftStep = SetBar(leftBar, leftStep);
        }
        if (isRightStart)
        {
            rightStep = SetBar(rightBar, rightStep);
        }
#endif

    }
    /*
    // ボタンのクリックでバーを停止するための関数
    private void StopBar(Image point, Image crit, Image bar, StateUI text)
    {
        if (isLeftStart || isRightStart)
        {
            //isStop=true;
            isLeftStart = false;
            isRightStart = false;
            text.StateDisplay(CheckPosition(point, crit, bar));
            StartCoroutine(ResetButton());
        }
    }*/
    private void ResetBar(Image target)
    {
        var pos = target.rectTransform.localPosition;
        pos.x = minBar;
        target.rectTransform.localPosition = pos;
    }
    private float SetBar(Image target, float step)
    {
        step += Time.deltaTime * speed;
        var pos = target.rectTransform.localPosition;
        pos.x = Mathf.Lerp(minBar, maxBar, step);
        target.rectTransform.localPosition = pos;
        return step;
    }

    private void SetPoint(Image target,Image critical, float range)
    {
        var pos = target.rectTransform.localPosition;
        pos.x = Random.Range(minPoint, maxPoint);
        target.rectTransform.localPosition = pos;

        critical.rectTransform.localPosition = pos;

        var size = target.rectTransform.sizeDelta;
        size.x = range;
        target.rectTransform.sizeDelta = size;

        size.x /= criticalPoint;
        critical.rectTransform.sizeDelta = size;
    }
    private IEnumerator ResetButton()
    {
        Debug.Log("Reset");
        isLeftStart = false;
        isRightStart = false;
        isStopLeft = false;
        isStopRight = false;
        yield return new WaitForSeconds(1.0f);
       
        Initialization();
    }

    private float SetRange()
    {
        float range = pointRange / (1.0f + (0.1f * sec.SpeedIndex));
        return range;
    }

    private void Initialization()
    {
        //isStop = false;
        leftStep = 0f;
        rightStep = 0f;
        ResetBar(leftBar);
        ResetBar(rightBar);
        float range = SetRange();
        SetPoint(leftPoint, leftCritical, range);
        SetPoint(rightPoint, rightCritical, range);
    }
    private  TIMING_STATE CheckPosition(Image point, Image crit, Image bar)
    {
        float barPos = bar.rectTransform.localPosition.x;
        
        float critMin = crit.rectTransform.localPosition.x - 
            crit.rectTransform.sizeDelta.x / 2;
        float critMax = crit.rectTransform.localPosition.x + 
            crit.rectTransform.sizeDelta.x / 2;
        bool isCritical = critMin <= barPos && barPos <= critMax;
        if (isCritical) 
        {
            sec.AddSpeed();
            audioSource.PlayOneShot(sound1);
            return TIMING_STATE.Great;
           
        }
        
        float pointMin = point.rectTransform.localPosition.x - point.rectTransform.sizeDelta.x / 2;
        float pointMax = point.rectTransform.localPosition.x + point.rectTransform.sizeDelta.x / 2;
        bool isPoint = pointMin <= barPos && barPos <= pointMax;
        if (isPoint)
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
