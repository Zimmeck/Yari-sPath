using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Vector2 lastCheckPointPos;
    public string CMactive;
    public GameObject vcParent;
    public GameObject startPos;
    public Vector2 mapLastPosition;
    public int collectibleCount;
    public string whatLevelCompleted;


    public int bouncy;
    public int bomb;
    public int fire;
    public int stay;

    public bool bouncySelected;
    public bool bombSelected;
    public bool fireSelected;
    public bool staySelected;

    public void ResetAbilities()
    {
        bouncy = 0;
        bomb = 0;
        fire = 0;
        stay = 0;

    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        if (startPos != null)
        {
            lastCheckPointPos = startPos.transform.position;
        }
    }


    private void Start()
    {
        collectibleCount = PlayerPrefs.GetInt("collectibleCount");

        bouncy = PlayerPrefs.GetInt("bouncySpear");
        bomb = PlayerPrefs.GetInt("bombSpear");
        fire = PlayerPrefs.GetInt("fireSpear");
        stay = PlayerPrefs.GetInt("staySpear");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
