using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]private Vector3 generatePos = new Vector3(0, 7f, 0);
    private void Awake()
    {
        Locator<Enemy>.Bind(this);
    }
    private void OnDestroy()
    {
        Locator<Enemy>.Unbind(this);
    }
    public void spawn()
    {
        Debug.Log("a");
        Instantiate(enemy, generatePos, Quaternion.identity);
    }
}
