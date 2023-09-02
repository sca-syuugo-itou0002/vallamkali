using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManagerTest : MonoBehaviour
{
    [SerializeField] private Text moveDistanceText;
    private float totalMoveDistance = 0f; //���v�ړ�����
    [SerializeField] private Text scoreText;
    private int totalScore = 0; //���v�X�R�A 
    private float currentSpeed = 0f;
    private void OnEnable()
    {
        Locator<ScoreManagerTest>.Bind(this);
        UpdateText();
    }
    private void OnDisable()
    {
        Locator<ScoreManagerTest>.Unbind(this);
    }

    public virtual void AddMoveDistance(float speed)
    {
        currentSpeed = speed;
        UpdateText();
    }
    public virtual void AddScore()
    {
        totalScore++;
        UpdateText();
    }
    // Update is called once per frame
    void Update()
    {
        totalMoveDistance += Time.deltaTime * currentSpeed;
        UpdateText();
    }
    private void UpdateText()
    {
        moveDistanceText.text = "�ړ�����:" + (totalMoveDistance / 100f).ToString("F1") + "m";
        scoreText.text = "�ǂ��z�����G:" + totalScore.ToString("F0");
    }
}
