using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManagerTest : MonoBehaviour
{
    public static ScoreManagerTest Instance
    {
        get; private set;
    }
    //[SerializeField] private Text moveDistanceText;
    [SerializeField] private TextMeshProUGUI _moveDistanceText;
    public float totalMoveDistance = 0f; //合計移動距離
    //[SerializeField] private Text scoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Text FinishText;
    public int totalScore = 0; //合計スコア 
    private float currentSpeed = 0f;
    [SerializeField] public int timeLimit;
    //[SerializeField] private Text CountTimeText;
    [SerializeField] private TextMeshProUGUI _CountTimeText;
    private float time;
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
    private void Start()
    {
        FinishText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        totalMoveDistance += ((Time.deltaTime * currentSpeed)/10f);
        UpdateText();
    }
    public void UpdateText()
    {
        _moveDistanceText.text = totalMoveDistance.ToString("F1");
        _scoreText.text =  totalScore.ToString("F0");
        int remaining = timeLimit - (int)time;
        _CountTimeText.text = remaining.ToString("D3");
        if (remaining == 0)
        {
            FinishText.enabled = true;
            FinishText.text = ("Finish!");
            new WaitForSeconds(2.0f);
            SceneManager.LoadScene("Resulit", LoadSceneMode.Single);
        }
        resulitdistance=totalMoveDistance;
        resulitscore=totalScore;
    }
}
