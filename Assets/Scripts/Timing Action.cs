using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TimingAction : MonoBehaviour
{
    [SerializeField] private Image leftPoint;//左側の止めるべき位置
    [SerializeField] private Image leftBar;//左側のバー
    [SerializeField] private Image rightPoint;//右側の止めるべき位置
    [SerializeField] private Image rightBar;//右側のバー

    [SerializeField] private float minY;//最小値
    [SerializeField] private float maxY;//最大値
    private float leftStep;//左止め
    private float rightStep;//右止め
    private float leftPosition;
    private float rightPosition;
    [SerializeField] private float speed;

    private bool isLeftStart = false;
    private bool isRightStart = false;
    void Awake()
    {
        ResetBar(leftBar);
        ResetBar(rightBar);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ResetBar(leftBar);
            leftStep = 0f;
            isLeftStart = true;
            if (Input.GetMouseButtonUp(0))
            {
                isLeftStart = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //ResetBar(rightBar);

            rightStep = 0f;
            isRightStart = true;
            if (Input.GetMouseButtonUp(0))
            {
                isRightStart = false;
            }
        }

        if (isLeftStart)
        {
            leftStep = SetBar(leftBar, leftStep);
        }
        if (isRightStart)
        {
            rightStep = SetBar(rightBar, rightStep);
        }
    }
    private void ResetBar(Image target)
    {
        var pos = target.rectTransform.localPosition;
        pos.x = minY;
        target.rectTransform.localPosition = pos;
    }
    private float SetBar(Image target, float step)
    {
        step += Time.deltaTime * speed;
        var pos = target.rectTransform.localPosition;
        pos.x = Mathf.Lerp(minY, maxY, step);
        target.rectTransform.localPosition = pos;
        return step;
    }

    private void SetPoint(Image target)
    {

    }
}
