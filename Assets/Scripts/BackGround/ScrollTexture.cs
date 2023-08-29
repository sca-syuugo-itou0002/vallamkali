using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] private Material mat;
    Vector2 currentOffset;
    private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private float speedPer = 0f;
    public float SpeedPer
    {
        set { speedPer = value; }
    }
    [SerializeField] private float rescrollPos;
    void Update()
    {
        speed = Mathf.Lerp(minSpeed, maxSpeed, speedPer);
        currentOffset.y += speed * Time.deltaTime;
        if (currentOffset.y >= rescrollPos) currentOffset.y -= rescrollPos;
        mat.SetTextureOffset("_MainTex", currentOffset);
    }
}
