using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameManager gm;
    public GameObject startPosition;
    public GameObject vcParent;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        vcParent = GameObject.Find("VCParent");
        startPosition = GameObject.Find("StartPosition");

        gm.vcParent = vcParent;
        gm.startPos.transform.position = startPosition.transform.position;

        startPosition.transform.position = gm.lastCheckPointPos;

        for (int i = 0; i < vcParent.transform.childCount; i++)
        {
            if (gm.CMactive == vcParent.transform.GetChild(i).name)
            {
                GameObject.Find(gm.CMactive).SetActive(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
