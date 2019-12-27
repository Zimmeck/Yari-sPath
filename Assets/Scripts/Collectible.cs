using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public GameObject destroyEffect;
    public GameObject takeCollectibleEffect;
    private Transform target;
    private bool traveling;
    public float speed;
    public int collected;
    void DestroyCollectible()
    {
        if (collected == 0)
        {
            target.gameObject.GetComponentInParent<PlayerScr>().TakeCollectible();
        }

        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        //GameObject takeCollectibleEffectInstance = Instantiate(takeCollectibleEffect, target.position, Quaternion.identity);
        //takeCollectibleEffectInstance.transform.parent = target;
        PlayerPrefs.SetInt(gameObject.name, 1);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        collected = PlayerPrefs.GetInt(gameObject.name);

        if (collected == 1)
        {
            var tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.2f;
            GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (traveling)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.position) <= 0.2f)
            {
                if (target.gameObject.GetComponentInParent<PlayerScr>().isGrounded)
                {
                    DestroyCollectible();
                }
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject.transform;
            traveling = true;
        }
    }
}
