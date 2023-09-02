using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;

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
    private bool canStop = false;

    private int leftButtonClickCount = 0;
    private int rightButtonClickCount = 0;
    //UI
    [SerializeField] private StateUI leftText;
    [SerializeField] private StateUI rightText;

    public enum TIMING_STATE
    {
        Bad,
        Good,
        Great
    }
    void Awake()
    {
        Initialization();
    }
    // マウスのクリック操作をボタンに関連付けるための関数
    public void LeftButtonClicked()
    {
        if (leftButtonClickCount % 2 == 0)
        {
            isLeftStart = true;
        }
        else
        {
            isLeftStart = false;
            leftText.StateDisplay(CheckPosition(leftPoint, leftCritical, leftBar));
        }
        leftButtonClickCount++;
    }

    public void RightButtonClicked()
    {
        if (rightButtonClickCount % 2 == 0)
        {
            isRightStart = true;
        }
        else
        {
            isRightStart = false;
            rightText.StateDisplay(CheckPosition(rightPoint, rightCritical, rightBar));
        }
        rightButtonClickCount++;
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
        /*
        if (isRightStart == true || isLeftStart == true)
        {
            StartCoroutine(ResetButton());
        }*/
#if false
        canStop = isLeftStart && isRightStart;
        if (Input.GetMouseButtonDown(0) && !isLeftStart)
        {
            isLeftStart = true;
        }
        if (Input.GetMouseButtonDown(0) && !isRightStart)
        {   
            isRightStart = true;
        }
        if (Input.GetMouseButtonUp(0) && canStop)
        {
            leftText.StateDisplay(CheckPosition(leftPoint, leftCritical, leftBar));
            rightText.StateDisplay(CheckPosition(rightPoint, rightCritical, rightBar));
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
    // ボタンのクリックでバーを停止するための関数
    private void StopBar(Image point, Image crit, Image bar, StateUI text)
    {
        if (isLeftStart || isRightStart)
        {
            isLeftStart = false;
            isRightStart = false;
            text.StateDisplay(CheckPosition(point, crit, bar));
            StartCoroutine(ResetButton());
        }
    }
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
        isLeftStart = false;
        isRightStart = false;
        yield return new WaitForSeconds(1.0f);
        
        Initialization();
    }

    private float SetRange()
    {
        float range = pointRange / (1.0f + (0.1f * combos));
        return range;
    }

    private void Initialization()
    {
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
        if (isCritical) return TIMING_STATE.Great;
        float pointMin = point.rectTransform.localPosition.x - 
            point.rectTransform.sizeDelta.x / 2;
        float pointMax = point.rectTransform.localPosition.x + 
            point.rectTransform.sizeDelta.x / 2;
        bool isPoint = pointMin <= barPos && barPos <= pointMax;
        if(isPoint) return TIMING_STATE.Good;
        else return TIMING_STATE.Bad;
    }
}
