using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosionSoundObj;
    public SpriteRenderer mySprite;
    public GameObject explossionPrefab;
    public GameObject smokePrefab;
    public bool explode;
    public bool exploding;
    public float delayTime;

    public float timeToExplode;
    public float startTimeToExplode;
    IEnumerator ExplodeBombInTime()
    {
        if (!exploding)
        {
            yield return new WaitForSeconds(delayTime);
            Detonate();
        }
    }


    //public IEnumerator ExplodeBombTimer()
    //{
    //    yield return new WaitForSeconds(1f);

    //    if (gameObject != null)
    //    {
    //        Detonate();
    //    }

    //}

    public void ExplodeBombTimerFloat()
    {
        if (!exploding)
        {
            exploding = true;
            timeToExplode = startTimeToExplode;
        }

    }

    public void Detonate()
    {
        Camera.main.gameObject.GetComponent<CameraBehaviour>().DoCameraShake();
        GameObject explosionInstance = Instantiate(explossionPrefab, transform.position, Quaternion.identity);
        Instantiate(smokePrefab, transform.position, Quaternion.identity);
        FindObjectOfType<VibrationManager>().Vibrate(.5f,.5f,.1f);
        Instantiate(explosionSoundObj, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (exploding)
        {
            timeToExplode -= Time.deltaTime;
            mySprite.color = Color.red;
        }

        if (explode)
        {
            explode = false;
            //StartCoroutine(ExplodeBombTimer());
            ExplodeBombTimerFloat();

        }

        if (timeToExplode < 0)
        {

            Detonate();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explossion>() != null)
        {
            exploding = true;
            StartCoroutine(ExplodeBombInTime());
        }
    }
}
