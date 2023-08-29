using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SlotController : MonoBehaviour
{
    [SerializeField] private Image leftPoint;
    [SerializeField] private Image leftBar;
    [SerializeField] private Image rightPoint;
    [SerializeField] private Image rightBar;

    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private float leftStep;
    private float rightStep;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //ResetBar(leftBar);
            leftStep = 0f;
            isLeftStart = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //ResetBar(rightBar);

            rightStep = 0f;
            isRightStart = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isLeftStart = false;
            isRightStart = false;
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
