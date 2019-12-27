using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    public string abilityToPick;
    private PlayerScr playerRef;
    public bool isSelected;
    private Animator myAnim;
    public GameObject myImage;

    public void PickAbility()
    {
        if (Input.GetAxisRaw("LeftTrigger") == 1)
        {
            if (abilityToPick == "bouncy")
            {
                playerRef.nextSpearIsBouncy = true;
            }

            if (abilityToPick == "bomb")
            {
                playerRef.nextSpearIsBomb = true;
            }

            if (abilityToPick == "fire")
            {
                playerRef.nextSpearIsFire = true;
            }

            if (abilityToPick == "stay")
            {
                playerRef.nextSpearIsStay = true;
            }
        }
    }

    public void UnPickAbility()
    {
        if (Input.GetAxis("LeftTrigger") == 1)
        {
            if (abilityToPick == "bouncy")
            {
                playerRef.nextSpearIsBouncy = false;
            }

            if (abilityToPick == "bomb")
            {
                playerRef.nextSpearIsBomb = false;
            }

            if (abilityToPick == "fire")
            {
                playerRef.nextSpearIsFire = false;
            }

            if (abilityToPick == "stay")
            {
                playerRef.nextSpearIsStay = false;
            }
        }
    }

    public void EnableObj()
    {
        GetComponent<Image>().enabled = true;
        if (GetComponentInChildren<Text>() != null)
        {
            GetComponentInChildren<Text>().enabled = true;
        }

        myImage.SetActive(true);
    }
    public void DisableObj()
    {
        GetComponent<Image>().enabled = false;
        if (GetComponentInChildren<Text>() != null)
        {
            GetComponentInChildren<Text>().enabled = false;
        }

        myImage.SetActive(false);
    }


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        playerRef = FindObjectOfType<PlayerScr>();

        if (abilityToPick == "bouncy")
        {
            if (FindObjectOfType<GameManager>().bouncy == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }

        if (abilityToPick == "bomb")
        {
            if (FindObjectOfType<GameManager>().bomb == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }

        if (abilityToPick == "fire")
        {
            if (FindObjectOfType<GameManager>().fire == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }

        if (abilityToPick == "stay")
        {
            if (FindObjectOfType<GameManager>().stay == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (abilityToPick == "bouncy")
        {
            if (FindObjectOfType<GameManager>().bouncy == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }

        if (abilityToPick == "bomb")
        {
            if (FindObjectOfType<GameManager>().bomb == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }

        if (abilityToPick == "fire")
        {
            if (FindObjectOfType<GameManager>().fire == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }


        if (abilityToPick == "stay")
        {
            if (FindObjectOfType<GameManager>().stay == 1)
            {
                EnableObj();
            }
            else
            {
                DisableObj();
            }
        }


        if (isSelected)
        {
            myAnim.SetBool("Selected", true);
        }
        else
        {
            myAnim.SetBool("Selected", false);
        }
    }

    //private void OnEnable()
    //{
    //    isSelected = false;
    //    GetComponent<Animator>().SetBool("Selected", false);
    //}
}
