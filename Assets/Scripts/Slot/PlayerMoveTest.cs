using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Locator<ScoreManagerTest>.Instance.AddScore();
            Destroy(other.gameObject);
        }
    }
    public void SwitchResultScene()
    {

    }
}
