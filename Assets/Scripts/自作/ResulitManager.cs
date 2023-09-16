using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResulitManager : MonoBehaviour
{
    [SerializeField] private Text ResulitmoveDistance;
    public float ResulitMoveDistance;
    [SerializeField] private Text Resulitscore;
    public int ResulitScore;
    
    // Start is called before the first frame update
    void Start()
    {
        ResulitMoveDistance=ScoreManagerTest.getresulitdistance();
        ResulitScore=ScoreManagerTest.getreusltscore();
        ResulitmoveDistance.text= "ˆÚ“®‹——£:" + ResulitMoveDistance.ToString("F1")+"m";
        Resulitscore.text= "’Ç‚¢‰z‚µ‚½“G:" + ResulitScore.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
