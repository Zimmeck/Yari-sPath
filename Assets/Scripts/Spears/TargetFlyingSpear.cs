using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFlyingSpear : MonoBehaviour
{
    public GameObject flyingSpearPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject myFlyingSpear = Instantiate(flyingSpearPrefab,transform.position, Quaternion.identity);
        myFlyingSpear.GetComponent<MoveTowardsPoint>().target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
