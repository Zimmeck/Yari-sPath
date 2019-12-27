using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    private GameManager gm;
    public int whatPathThisUnblock;

    IEnumerator DoWalkOut()
    {
        GameObject.Find("CM_FollowPlayer").SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        gm.whatLevelCompleted = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(gm.whatLevelCompleted, whatPathThisUnblock);
        PlayerPrefs.SetInt("collectibleCount", gm.collectibleCount);
        FindObjectOfType<PlayerScr>().GoToLevelSlector();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerScr>().canControlPlayer = false;
            StartCoroutine(DoWalkOut());

        }
    }
}
