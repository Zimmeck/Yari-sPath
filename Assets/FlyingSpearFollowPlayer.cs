using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpearFollowPlayer : MonoBehaviour
{
    private Transform playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<PlayerScr>().gameObject.transform;
        transform.position = GetComponent<MoveTowardsPoint>().target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
            transform.eulerAngles = new Vector3(0,0,-12.5f);
        }

        if (playerRef.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.eulerAngles = new Vector3(0, 0, 12.5f);
        }

    }
}
