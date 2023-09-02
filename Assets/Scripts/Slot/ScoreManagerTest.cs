using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManagerTest : MonoBehaviour
{
    [SerializeField] private Text moveDistanceText;
    private float totalMoveDistance = 0f; //合計移動距離
    [SerializeField] private Text scoreText;
    private int totalScore = 0; //合計スコア 
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
        moveDistanceText.text = "移動距離:" + (totalMoveDistance / 100f).ToString("F1") + "m";
        scoreText.text = "追い越した敵:" + totalScore.ToString("F0");
    }
}
