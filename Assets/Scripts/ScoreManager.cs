using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject score_object = null; // Text�I�u�W�F�N�g
    public int score_num = 0; // �X�R�A�ϐ�
    // Start is called before the first frame update
    void Start()
    {
        // �X�R�A�̃��[�h
        //score_num = PlayerPrefs.GetInt("SCORE", 0);
    }
    // �폜���̏���
    void OnDestroy()
    {
        // �X�R�A��ۑ�
        //PlayerPrefs.SetInt("SCORE", score_num);
        //PlayerPrefs.Save();
    }
    // Update is called once per frame
    public void SetScore()
    {
        // �I�u�W�F�N�g����Text�R���|�[�l���g���擾
        Text score_text = score_object.GetComponent<Text>();
        score_num += 1; // �Ƃ肠����1���Z�������Ă݂�
        // �e�L�X�g�̕\�������ւ���
        score_text.text = "Score:" + score_num;

        if (score_num >= 10)
        {
            SceneManager.LoadScene("Resulit");
        }
    }
}
