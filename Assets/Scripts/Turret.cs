using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float startTimeToShot1;
    private float timeToShot1;

    public float startTimeToShotRafaga;
    private GameObject player;


    void Shot1()
    {
        GameObject myBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        timeToShot1 = startTimeToShot1;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScr>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= 200)
        {
            if (timeToShot1 <= 0)
            {
                Shot1();
            }
            else
            {
                timeToShot1 -= Time.deltaTime;
            }
        }


    }
}
