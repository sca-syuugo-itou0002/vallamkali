using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float stopYPosition = 1.0f;
    [SerializeField] private Animator anim;
    private bool isHit = false;
    [SerializeField] private Vector2 targetPos;
    private Vector2 oldPos;
    private float hitTime = 0.8f;
    private float currentTime = 0f;

    void FixedUpdate()
    {
        if (isHit)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(oldPos, targetPos, currentTime / hitTime);
            return;
        }
        if (transform.position.y > stopYPosition)
        {
            transform.Translate(0, -0.015f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isHit)
        {
            isHit = true;
            anim.SetTrigger("OnPlayerHit");
            oldPos = transform.position;
            StartCoroutine(DestroyDelay());
        }
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
        Locator<Enemy>.Instance.spawn();
        Locator<ScoreManagerTest>.Instance.AddScore();
    }
}
