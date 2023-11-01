using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResulitManager : MonoBehaviour
{
    //[SerializeField] private Text ResulitmoveDistance;
    [SerializeField] private TextMeshProUGUI _ResulitmoveDistance;
    public float ResulitMoveDistance;
    //[SerializeField] private Text Resulitscore;
    [SerializeField] private TextMeshProUGUI _Resulitscore;
    public int ResulitScore;
    
    // Start is called before the first frame update
    void Start()
    {
        ResulitMoveDistance=ScoreManagerTest.getresulitdistance();
        ResulitScore=ScoreManagerTest.getreusltscore();
        _ResulitmoveDistance.text= ResulitMoveDistance.ToString("F1");
        _Resulitscore.text= ResulitScore.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
