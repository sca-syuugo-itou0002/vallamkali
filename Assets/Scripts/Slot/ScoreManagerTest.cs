using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManagerTest : MonoBehaviour
{
    public static ScoreManagerTest Instance
    {
        get; private set;
    }
    [SerializeField] private Text moveDistanceText;
    public float totalMoveDistance = 0f; //���v�ړ�����
    [SerializeField] private Text scoreText;
    public int totalScore = 0; //���v�X�R�A 
    private float currentSpeed = 0f;
    [SerializeField] int timeLimit;
    [SerializeField] private Text CountTimeText;
    float time;
    public static float resulitdistance;
    public static int resulitscore;
    public static float getresulitdistance()
    {
        return resulitdistance;
    }
    public static int getreusltscore()
    {
        return resulitscore;
    }
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
        time += Time.deltaTime;
        totalMoveDistance += ((Time.deltaTime * currentSpeed)/100f);
        UpdateText();
    }
    public void UpdateText()
    {
        moveDistanceText.text = "�ړ�����:" + totalMoveDistance.ToString("F1") + "m";
        scoreText.text = "�ǂ��z�����G:" + totalScore.ToString("F0");
        int remaining = timeLimit - (int)time;
        CountTimeText.text = "�������ԁF"+remaining.ToString("D3");
        if (remaining == 0)
        {
            SceneManager.LoadScene("Resulit", LoadSceneMode.Single);
        }
        resulitdistance=totalMoveDistance;
        resulitscore=totalScore;
    }
}
