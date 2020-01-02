using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAbWithAngle : MonoBehaviour
{
    private GameManager gm;

    private float hAxis;
    private float vAxis;
    public GameObject upSlot;
    public GameObject rightSlot;
    public GameObject downSlot;
    public GameObject leftSlot;

    public int abilityPickedCount;
    public int maxAbilities;

    public void ResetSelectedAb()
    {
        upSlot.GetComponent<AbilitySlot>().isSelected = false;
        rightSlot.GetComponent<AbilitySlot>().isSelected = false;
        downSlot.GetComponent<AbilitySlot>().isSelected = false;
        leftSlot.GetComponent<AbilitySlot>().isSelected = false;
        abilityPickedCount = 0;
    }

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm.bouncySelected)
        {
            upSlot.GetComponent<AbilitySlot>().isSelected = true;
            upSlot.GetComponent<AbilitySlot>().PickAbility();
            abilityPickedCount += 1;
        }

        if (gm.bombSelected)
        {
            rightSlot.GetComponent<AbilitySlot>().isSelected = true;
            rightSlot.GetComponent<AbilitySlot>().PickAbility();
            abilityPickedCount += 1;
        }

        if (gm.fireSelected)
        {
            downSlot.GetComponent<AbilitySlot>().isSelected = true;
            downSlot.GetComponent<AbilitySlot>().PickAbility();
            abilityPickedCount += 1;
        }

        if (gm.staySelected)
        {
            leftSlot.GetComponent<AbilitySlot>().isSelected = true;
            leftSlot.GetComponent<AbilitySlot>().PickAbility();
            abilityPickedCount += 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();



    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        if (Input.GetAxis("LeftTrigger") == 1)
        {
            if (hAxis == 0 && vAxis == 1)
            {
                upSlot.GetComponent<Animator>().SetBool("Hover", true);
                if (Input.GetButtonDown("Jump"))
                {
                    if (upSlot.GetComponent<Image>().enabled)
                    {
                        if (upSlot.GetComponent<AbilitySlot>().isSelected == false)
                        {
                            if (abilityPickedCount < maxAbilities)
                            {
                                upSlot.GetComponent<AbilitySlot>().isSelected = true;
                                upSlot.GetComponent<AbilitySlot>().PickAbility();
                                abilityPickedCount += 1;
                            }
                        }
                        else
                        {
                            upSlot.GetComponent<AbilitySlot>().isSelected = false;
                            upSlot.GetComponent<AbilitySlot>().UnPickAbility();
                            abilityPickedCount -= 1;
                        }
                    }
                }
            }
            else
            {
                upSlot.GetComponent<Animator>().SetBool("Hover", false);
            }




            if (hAxis == 1 && vAxis == 0)
            {
                rightSlot.GetComponent<Animator>().SetBool("Hover", true);
                if (Input.GetButtonDown("Jump"))
                {
                    if (rightSlot.GetComponent<Image>().enabled)
                    {
                        if (rightSlot.GetComponent<AbilitySlot>().isSelected == false)
                        {
                            if (abilityPickedCount < maxAbilities)
                            {
                                rightSlot.GetComponent<AbilitySlot>().isSelected = true;
                                rightSlot.GetComponent<AbilitySlot>().PickAbility();
                                abilityPickedCount += 1;
                            }
                        }
                        else
                        {
                            rightSlot.GetComponent<AbilitySlot>().isSelected = false;
                            rightSlot.GetComponent<AbilitySlot>().UnPickAbility();
                            abilityPickedCount -= 1;
                        }
                    }
                }
            }
            else
            {
                rightSlot.GetComponent<Animator>().SetBool("Hover", false);
            }

            if (hAxis == 0 && vAxis == -1)
            {
                downSlot.GetComponent<Animator>().SetBool("Hover", true);
                if (Input.GetButtonDown("Jump"))
                {
                    if (downSlot.GetComponent<Image>().enabled)
                    {
                        if (downSlot.GetComponent<AbilitySlot>().isSelected == false)
                        {
                            if (abilityPickedCount < maxAbilities)
                            {
                                downSlot.GetComponent<AbilitySlot>().isSelected = true;
                                downSlot.GetComponent<AbilitySlot>().PickAbility();
                                abilityPickedCount += 1;
                            }
                        }
                        else
                        {
                            downSlot.GetComponent<AbilitySlot>().isSelected = false;
                            downSlot.GetComponent<AbilitySlot>().UnPickAbility();
                            abilityPickedCount -= 1;
                        }
                    }
                }
            }
            else
            {
                downSlot.GetComponent<Animator>().SetBool("Hover", false);
            }


            if (hAxis == -1 && vAxis == 0)
            {
                leftSlot.GetComponent<Animator>().SetBool("Hover", true);
                if (Input.GetButtonDown("Jump"))
                {
                    if (leftSlot.GetComponent<Image>().enabled)
                    {
                        if (leftSlot.GetComponent<AbilitySlot>().isSelected == false)
                        {
                            if (abilityPickedCount < maxAbilities)
                            {
                                leftSlot.GetComponent<AbilitySlot>().isSelected = true;
                                leftSlot.GetComponent<AbilitySlot>().PickAbility();
                                abilityPickedCount += 1;
                            }
                        }
                        else
                        {
                            leftSlot.GetComponent<AbilitySlot>().isSelected = false;
                            leftSlot.GetComponent<AbilitySlot>().UnPickAbility();
                            abilityPickedCount -= 1;
                        }
                    }
                }
            }
            else
            {
                leftSlot.GetComponent<Animator>().SetBool("Hover", false);
            }
        }

        
    }
}
