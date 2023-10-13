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
    public float targetWidth = 0f; // 最終的な幅
    public float targetHeight = 0f; // 最終的な高さ
    private Vector3 startScale;
    private float startTime;
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
        if (juge1 != null&&juge2!=null)
        {
            startScale = juge1.transform.localScale;
            startTime = Time.time;
            StartCoroutine(ScaleObjectOverTime());
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }
    }
    void Awake()
    {
        Initialization();
        pm = FindObjectOfType<PlayerMoveTest>();
       
    }
    IEnumerator ScaleObjectOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newWidth = Mathf.Lerp(startScale.x, targetWidth, t);
            float newHeight = Mathf.Lerp(startScale.y, targetHeight, t);
            juge1.transform.localScale = new Vector3(newWidth, newHeight, 1.0f);
            juge2.transform.localScale = new Vector3(newWidth, newHeight, 1.0f);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        // 確実に最終的なサイズを設定
        juge1.transform.localScale = new Vector3(targetWidth, targetHeight, 1.0f);
    }
    // マウスのクリック操作をボタンに関連付けるための関数
    public void LeftButtonClicked()
    {
            isStopLeft=true;
            StopJudge();
    }

    public void RightButtonClicked()
    {
            isStopRight=true;
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
    void Update()
    {


    }
    private IEnumerator ResetButton()
    {
        isStopLeft = false;
        isStopRight = false;
        yield return new WaitForSeconds(1.0f);
       
        Initialization();
    }


    private void Initialization()
    {
        
        leftStep = 0f;
        rightStep = 0f;
    }
    private  TIMING_STATE CheckPosition(Image point, Image crit, Image bar)
    {
        float barPos = bar.rectTransform.localPosition.x;
        
        float critMin = crit.rectTransform.localPosition.x - 
            crit.rectTransform.sizeDelta.x / 2;
        float critMax = crit.rectTransform.localPosition.x + 
            crit.rectTransform.sizeDelta.x / 2;
        bool isCritical = juge1;
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
