using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void SwitchMainScene()
    {
        SceneManager.LoadScene("MainGame",LoadSceneMode.Single);
    }
    public void SwitchTitelScene()
    {
        SceneManager.LoadScene("Titel", LoadSceneMode.Single);
    }
}
