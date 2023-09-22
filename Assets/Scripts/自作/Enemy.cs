using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void spawn()
    {
        Instantiate(enemy);
        transform.position=new Vector3(0,5,0);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
