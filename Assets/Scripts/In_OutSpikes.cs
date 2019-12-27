using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_OutSpikes : MonoBehaviour
{
    public float speed;
    public float StartTimeToOpenSpikes;
    public float timeToOpenSpikes;
    public Transform openPosition;
    public Transform startPosotion;
    public bool open;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.position = Vector2.MoveTowards(transform.position, openPosition.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosotion.position, speed * Time.deltaTime);
        }

        if (timeToOpenSpikes <= 0)
        {
            if (open)
            {
                open = false;
            }
            else
            {
                open = true;
            }
            timeToOpenSpikes = StartTimeToOpenSpikes;
        }
        else
        {
            timeToOpenSpikes -= Time.deltaTime;
        }
    }
    
}
