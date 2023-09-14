using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    GameObject playerObj;
    PlayerMoveTest player;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerMoveTest>();
        playerTransform = playerObj.transform;
    }

    void LateUpdate()
    {
        MoveCamera();
    }
    void MoveCamera()
    {
        //ècï˚å¸ÇæÇØí«è]
        transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
