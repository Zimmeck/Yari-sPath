using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform positions;
    public Transform actualPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, actualPos.position, speed * Time.deltaTime);
        transform.Rotate(0, 0, 20);

        if (transform.position == actualPos.transform.position)
        {
            if (positions.GetChild(positions.childCount - 1) == actualPos)
            {
                actualPos = positions.GetChild(0);
            }
            else
            {
                actualPos = positions.GetChild(actualPos.GetSiblingIndex() + 1);
            }
        }
    }
}
