using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float stopYPosition = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > stopYPosition)
        {
            transform.Translate(0, -0.015f, 0);
        }
    }
    public void enemymove()
    {
        
    }
}
