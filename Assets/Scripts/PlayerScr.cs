using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerScr : MonoBehaviour
{
    private GameManager gm;
    public VibrationManager vibrationManager;
    private CinemachineVirtualCamera myVC;

    public float mana;
    public float maxMana;
    public int maxSpears;
    public float moveSpeed;
    public float changingRoomSpeed;
    public float jumpSpeed;
    public float bouncySpearJumpSpeed;
    public float edgeClimbJumpSpeed;
    public float distanceToThrow;
    public float StartTimeToThrow;
    public float groundCheckDistance;
    public float distanceRayForLadder;
    public float pickThingDistance;
    public float dropForce;
    public float standardGravityScale;
    public float goingDownGravityScale;
    private float timeToLateJump;
    public float startTimeToLateJump;
    public float distanceToMove;
    public float distanceToDrop;
    private float timeToSpawnWalkEffect;
    public float startTimeToSpawnWalkEffect;
    public float maxFallVelocity;
    private float chargeSpearTime;
    public float maxSpearCharge;
    private float chargeEspecialSpearTime;
    public float maxChargeEspecialSpearTime;
    private bool chargingEspecialSpear;
    private float movingCameraTimer;
    public float moveCamSpeed;

    public float earlyJumpDetection;
    private float timeToRespawn;
    public float startTimeToRespawn;
    public float startTimeToHoldButton;
    public float timeToHoldButton;
    float timeToGetControlAfterBounce;

    private Rigidbody2D rb;
    private bool canThrow;
    private float timeToThrow;
    public bool isGrounded;
    private bool isGroundedOther;
    public bool isClimbing;
    private bool isPickingObject;
    public int collectibleCount;
    public bool canControlPlayer;
    public bool canLateJump;
    private bool checkedLateJump;
    public bool jumped;
    private bool chargingSpear;
    private RaycastHit2D ghostSpearHit;
    private bool fullChargeEffectMade;
    private bool hasCancelledThrow;
    private bool hasEarlyJumped;
    private bool inDyingTime;
    private bool inDyingFallingTime;
    private bool wheelPanelActive;
    private bool holdingButtonChecked;
    private bool canMove = true;
    private bool isNormalSpear;

    public Collider2D myCollider;
    public LayerMask platformLayer;
    public LayerMask walkableLayer;
    public LayerMask ladderLayer;
    public LayerMask objectLayer;
    public Transform startPosition;
    public Transform throwPoint;
    public Transform throwPointVerticalUp;
    public Transform throwPointVerticalDown;
    public Transform pickPoint;
    public GameObject spearsCountParent;
    public GameObject spearRPrefab;
    public GameObject spearLPrefab;
    public GameObject spearUPrefab;
    public Transform groundCheck;
    public Transform groundCheckR;
    public Transform groundCheckL;
    public GameObject brokeSpearParticles;
    public Animator myAnimator;
    public GameObject walkEffectParticles;
    public GameObject ghostSpearsObj;
    public GameObject chargeEffectParticles;
    public GameObject cantThrowSpearEffect;
    public GameObject[] backSpears;
    public GameObject landingParticlesEffect;
    public GameObject vcParent;
    public GameObject backSpearsObject;
    public GameObject wheelPanel;
    private GameObject newSpear;
    private Animator wheelAnim;
    private GameObject prevSpear;
    private GameObject pickedObject;
    private RaycastHit2D wallInfoRun;
    private RaycastHit2D wallInfoDropObj;

    public int bouncySpearActive;
    public int bombSpearActive;
    public int fireSpearActive;
    public int staySpearActive;

    public bool nextSpearIsBouncy;
    public bool nextSpearIsBomb;
    public bool nextSpearIsFire;
    public bool nextSpearIsStay;

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //}


    void GetAbilitiesFromGm()
    {
        nextSpearIsBouncy = gm.bouncySelected;
        nextSpearIsBomb = gm.bombSelected;
        nextSpearIsFire = gm.fireSelected;
        nextSpearIsStay = gm.staySelected;

        gm.bouncySelected = false;
        gm.bombSelected = false;
        gm.fireSelected = false;
        gm.staySelected = false;
    }
    public void ResetAbilities()
    {
        nextSpearIsBouncy = false;
        nextSpearIsBomb = false;
        nextSpearIsFire = false;
        nextSpearIsStay = false;
    }

    public IEnumerator ToggleWheel()
    {
        if (!wheelPanelActive)
        {
            wheelPanelActive = true;
            wheelPanel.SetActive(true);
        }
        else
        {
            wheelPanelActive = false;
            yield return new WaitForSeconds(.1f);
            wheelPanel.SetActive(false);
        }
    }
    public void GoToLevelSlector()
    {
        gm.lastCheckPointPos = Vector2.zero;
        SceneManager.LoadScene("DespearateWorld");
    }

    public void Die()
    {
        gm.lifes -= 1;
        myAnimator.SetBool("Dying", true);
        backSpearsObject.GetComponent<Rigidbody2D>().simulated = true;
        backSpearsObject.GetComponent<Collider2D>().isTrigger = false;

        timeToRespawn = startTimeToRespawn;
        inDyingTime = true;
        rb.isKinematic = true;
        myCollider.enabled = false;
        canControlPlayer = false;


        
        //Save spear selected
        if (nextSpearIsBouncy)
        {
            gm.bouncySelected = true;
        }

        if (nextSpearIsBomb)
        {
            gm.bombSelected = true;
        }

        if (nextSpearIsFire)
        {
            gm.fireSelected = true;
        }

        if (nextSpearIsStay)
        {
            gm.staySelected = true;
        }
    }

    public void DieFalling()
    {
        gm.lifes -= 1;
        myAnimator.SetBool("Dying", true);
        backSpearsObject.GetComponent<Rigidbody2D>().simulated = true;
        backSpearsObject.GetComponent<Collider2D>().isTrigger = false;

        timeToRespawn = startTimeToRespawn;
        inDyingFallingTime = true;
        myCollider.enabled = false;
        canControlPlayer = false;

        //Save spear selected
        if (nextSpearIsBouncy)
        {
            gm.bouncySelected = true;
        }

        if (nextSpearIsBomb)
        {
            gm.bombSelected = true;
        }

        if (nextSpearIsFire)
        {
            gm.fireSelected = true;
        }

        if (nextSpearIsStay)
        {
            gm.staySelected = true;
        }
    }

    private void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Jump(float jumpForce)
    {

        timeToLateJump = 0;
        //rb.gravityScale = standardGravityScale;
        jumped = true;
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public IEnumerator HorizontalBounce(float jumpForce)
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(jumpForce, jumpSpeed);
        yield return new WaitForSeconds(.3f);
        canMove = true;
    }

    public void HorizontalBouncy(float jumpForce)
    {
        canMove = false;
        timeToGetControlAfterBounce = 0.4f;
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(jumpForce, jumpSpeed);
    }

    //private void DestroyLastSpear()
    //{
    //    if (!newSpear.GetComponent<SpearScr>().isStaySpear)
    //    {
    //        spearsCountParent.transform.GetChild(0).GetComponent<SpearScr>().DestroySpear();
    //    }

    //}

    private void DestroyLastSpearAnyways()
    {
        if (!Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
        {
            spearsCountParent.transform.GetChild(0).GetComponent<SpearScr>().DestroySpear();
        }
    }

    void ThrowSpear()
    {

        RaycastHit2D wallInfo = Physics2D.Raycast(throwPoint.position, Vector2.right * transform.localScale, distanceToThrow, platformLayer);
        RaycastHit2D floorInfo = Physics2D.Raycast(throwPointVerticalUp.position, Vector2.up, distanceToThrow, platformLayer);
        if (wallInfo == false)
        {
            if (Input.GetAxis("Vertical") <= 0.2f && Input.GetAxis("Vertical") >= -0.2f)
            {
                if (transform.localScale.x == 1)
                {
                    if (chargeSpearTime >= maxSpearCharge)
                    {
                        if (!hasCancelledThrow)
                        {
                            newSpear = Instantiate(spearRPrefab, throwPoint.position, Quaternion.identity);
                            newSpear.transform.parent = spearsCountParent.transform;

                            if (!isNormalSpear)
                            {
                                if (nextSpearIsBouncy)
                                {
                                    newSpear.GetComponent<SpearScr>().isBouncySpear = true;
                                    //nextSpearIsBouncy = false;
                                }
                                if (nextSpearIsBomb)
                                {
                                    newSpear.GetComponent<SpearScr>().isBombSpear = true;
                                    //nextSpearIsBomb = false;
                                }

                                if (nextSpearIsFire)
                                {
                                    newSpear.GetComponent<SpearScr>().isFireSpear = true;
                                    //nextSpearIsFire = false;
                                }

                                if (nextSpearIsStay)
                                {
                                    newSpear.GetComponent<SpearScr>().isStaySpear = true;
                                    //nextSpearIsStay = false;
                                }
                            }     
                        }
                    }
                }
                else if (transform.localScale.x == -1)
                {
                    GameObject newSpear;
                    if (chargeSpearTime >= maxSpearCharge)
                    {
                        if (!hasCancelledThrow)
                        {
                            newSpear = Instantiate(spearLPrefab, throwPoint.position, Quaternion.identity);
                            newSpear.transform.parent = spearsCountParent.transform;

                            if (!isNormalSpear)
                            {
                                if (nextSpearIsBouncy)
                                {
                                    newSpear.GetComponent<SpearScr>().isBouncySpear = true;
                                    //nextSpearIsBouncy = false;
                                }

                                if (nextSpearIsBomb)
                                {
                                    newSpear.GetComponent<SpearScr>().isBombSpear = true;
                                    //nextSpearIsBomb = false;
                                }


                                if (nextSpearIsFire)
                                {
                                    newSpear.GetComponent<SpearScr>().isFireSpear = true;
                                    //nextSpearIsFire = false;
                                }

                                if (nextSpearIsStay)
                                {
                                    newSpear.GetComponent<SpearScr>().isStaySpear = true;
                                    //nextSpearIsStay = false;
                                }
                            }     
                        }
                    }
                }
            }
        }
        else if (wallInfo && Input.GetAxis("Vertical") < 0.5)
        {
            if (!hasCancelledThrow)
            {
                Instantiate(brokeSpearParticles, throwPoint.position, Quaternion.identity);
                GameObject cantThrowSpearEffectInstance = Instantiate(cantThrowSpearEffect, ghostSpearsObj.transform.position, ghostSpearsObj.transform.rotation);
                cantThrowSpearEffectInstance.transform.localScale = ghostSpearsObj.transform.localScale;
                //ResetAbilities();
            }
        }

        if (floorInfo == false)
        {
            if (Input.GetAxis("Vertical") > 0.5)
            {
                GameObject newSpear;
                if (chargeSpearTime >= maxSpearCharge)
                {
                    if (!hasCancelledThrow)
                    {
                        newSpear = Instantiate(spearUPrefab, throwPointVerticalUp.position, Quaternion.identity);
                        newSpear.transform.parent = spearsCountParent.transform;


                        if (!isNormalSpear)
                        {
                            if (nextSpearIsBouncy)
                            {
                                newSpear.GetComponent<SpearScr>().isBouncySpear = true;
                                //nextSpearIsBouncy = false;
                            }

                            if (nextSpearIsBomb)
                            {
                                newSpear.GetComponent<SpearScr>().isBombSpear = true;
                                //nextSpearIsBomb = false;
                            }

                            if (nextSpearIsFire)
                            {
                                newSpear.GetComponent<SpearScr>().isFireSpear = true;
                                //nextSpearIsFire = false;
                            }

                            if (nextSpearIsStay)
                            {
                                newSpear.GetComponent<SpearScr>().isStaySpear = true;
                                //nextSpearIsStay = false;
                            }
                        }
                    }
                }
            }
        }
        else if (floorInfo && Input.GetAxis("Vertical") > 0.5)
        {
            if (!hasCancelledThrow)
            {
                Instantiate(brokeSpearParticles, throwPoint.position, Quaternion.identity);
                GameObject cantThrowSpearEffectInstance = Instantiate(cantThrowSpearEffect, ghostSpearsObj.transform.position, ghostSpearsObj.transform.rotation);
                cantThrowSpearEffectInstance.transform.localScale = ghostSpearsObj.transform.localScale;
                //ResetAbilities();
            }
        }

        //if (spearsCountParent.transform.childCount == maxSpears + 1)
        //{

        //        DestroyLastSpear();


        //    //if (spearsCountParent.transform.GetChild(spearsCountParent.transform.childCount - 1).GetComponent<SpearScr>().isStaySpear == false)
        //    //{

        //    //}
        //}


        //back spears
        //if (spearsCountParent.transform.childCount != 0)
        //{
        //    for (int i = 0; i <= spearsCountParent.transform.childCount -1; i++)
        //    {
        //        backSpears[i].SetActive(false);
        //    }
        //}




        timeToThrow = StartTimeToThrow;
        chargeSpearTime = 0;
        chargingSpear = false;
        chargingEspecialSpear = false;
        fullChargeEffectMade = false;

    }

    public void TakeCollectible()
    {
        collectibleCount += 1;
        gm.collectibleCount += collectibleCount;
    }

    public IEnumerator GoNextRoom()
    {
        canControlPlayer = false;

        yield return new WaitForSeconds(2f);
        canControlPlayer = true;

    }

    //void CheckIfIsNormalSpear()
    //{
    //    //AÑADIR AQUI TOAS LAS HABILIDADES
    //    if (nextSpearIsBouncy || nextSpearIsBomb || nextSpearIsFire || nextSpearIsStay)
    //    {

    //    }
    //}

    private void CheckHoldLeftTrigger()
    {
        if (!holdingButtonChecked)
        {
            if (Input.GetAxis("LeftTrigger") == 1)
            {
                timeToHoldButton += Time.deltaTime;
            }

            if (timeToHoldButton >= startTimeToHoldButton)
            {
                holdingButtonChecked = true;
                StartCoroutine(ToggleWheel());
            }
        }

        if (Input.GetAxis("LeftTrigger") == 0)
        {
            if (holdingButtonChecked)
            {
                holdingButtonChecked = false;
                timeToHoldButton = 0;
                StartCoroutine(ToggleWheel());
            }

        }

        if (Input.GetAxis("LeftTrigger") == 0)
        {
            timeToHoldButton = 0;
        }
    }

    private void GoToStartPosition()
    {
        if (gm.lifes == 0)
        {
            gm.lastCheckPointPos = startPosition.transform.position;
            gm.lifes = gm.savedLifes;
        }

        transform.position = gm.lastCheckPointPos;

        if (vcParent != null)
        {
            for (int i = 0; i < vcParent.transform.childCount; i++)
            {
                if (vcParent.transform.GetChild(i).name == gm.CMactive)
                {
                    vcParent.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    vcParent.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

    }

    private void Awake()
    {
        //controls = new PlayerInputs();

        //controls.Gameplay.Jump.performed += ctx => Jump(jumpSpeed);

        if (FindObjectOfType<VibrationManager>() == null)
        {
            Instantiate(vibrationManager);
        }
    }
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        myVC = GameObject.Find("CM_FollowPlayer").GetComponent<CinemachineVirtualCamera>();
        wheelAnim = wheelPanel.GetComponent<Animator>();
        GoToStartPosition();

        mana = maxMana;

        bouncySpearActive = PlayerPrefs.GetInt("bouncySpear");
        bombSpearActive = PlayerPrefs.GetInt("bombSpear");
        fireSpearActive = PlayerPrefs.GetInt("fireSpear");
        staySpearActive = PlayerPrefs.GetInt("staySpear");

        GetAbilitiesFromGm();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector2.down * earlyJumpDetection);
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (canControlPlayer)
        {
            wallInfoRun = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, distanceToMove, platformLayer);
            wallInfoDropObj = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, distanceToDrop, platformLayer);

            if (!wallInfoRun)
            {

                if (!wheelPanelActive)
                {
                    if (canMove)
                    {
                        rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }


                if (isGrounded)
                {
                    if (rb.velocity.x != 0)
                    {
                        if (timeToSpawnWalkEffect <= 0)
                        {
                            Instantiate(walkEffectParticles, groundCheck.transform.position + new Vector3(0f, -groundCheckDistance, 0f), Quaternion.identity);
                            timeToSpawnWalkEffect = startTimeToSpawnWalkEffect;
                        }
                    }
                }
            }
        }


        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxFallVelocity);
        //Deteccion de suelo y late jump

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, platformLayer);
        //isGroundedOther = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, walkableLayer);

        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, platformLayer)
        || Physics2D.Raycast(groundCheckR.position, Vector2.down, groundCheckDistance, platformLayer)
        || Physics2D.Raycast(groundCheckL.position, Vector2.down, groundCheckDistance, platformLayer))
        {
            isGrounded = true;
            checkedLateJump = false;
        }
        else
        {
            isGrounded = false;


            if (!canLateJump && !jumped && !checkedLateJump)
            {
                checkedLateJump = true;
                timeToLateJump = startTimeToLateJump;
            }
        }

        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, walkableLayer)
            || Physics2D.Raycast(groundCheckR.position, Vector2.down, groundCheckDistance, walkableLayer)
            || Physics2D.Raycast(groundCheckL.position, Vector2.down, groundCheckDistance, walkableLayer))
        {
            isGroundedOther = true;
            checkedLateJump = false;
        }
        else
        {
            isGroundedOther = false;

            if (!canLateJump && !jumped && !checkedLateJump)
            {

                timeToLateJump = startTimeToLateJump;
            }
        }

        if (timeToLateJump > 0 && !jumped && checkedLateJump)
        {
            canLateJump = true;
        }

        if (timeToLateJump <= 0 || jumped)
        {
            canLateJump = false;
        }

        timeToLateJump -= Time.deltaTime;

        //Animaation
        if (rb.velocity.x != 0)
        {
            myAnimator.SetBool("Moving", true);
        }
        if (rb.velocity.x == 0)
        {
            myAnimator.SetBool("Moving", false);

        }

        if (!isGrounded)
        {
            if (rb.velocity.y < 0)
            {
                myAnimator.SetBool("goingDown", true);

            }
        }
        if (rb.velocity.y > 0)
        {
            myAnimator.SetBool("goingDown", false);
        }

        if (isGrounded || isGroundedOther)
        {
            myAnimator.SetBool("isGrounded", true);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
        }




        //GhostSpears Feedback


        if (chargingSpear || chargingEspecialSpear)
        {
            ghostSpearsObj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        else
        {
            ghostSpearsObj.GetComponentInChildren<SpriteRenderer>().enabled = false;

        }

        if (Input.GetAxis("Vertical") > 0.2f)
        {
            ghostSpearHit = Physics2D.Raycast(throwPointVerticalUp.position, throwPoint.up, 1000000, platformLayer);
            ghostSpearsObj.transform.eulerAngles = new Vector3(ghostSpearsObj.transform.eulerAngles.x, ghostSpearsObj.transform.eulerAngles.y, 90);
        }
        else
        {
            ghostSpearHit = Physics2D.Raycast(throwPoint.position, throwPoint.right * transform.localScale.x, 1000000, platformLayer);
            ghostSpearsObj.transform.eulerAngles = new Vector3(ghostSpearsObj.transform.eulerAngles.x, ghostSpearsObj.transform.eulerAngles.y, 0);
        }


        if (ghostSpearsObj.transform.eulerAngles == new Vector3(0, 0, 0))
        {
            if (transform.localScale.x < 0)
            {
                ghostSpearsObj.transform.localScale = new Vector2(-1, ghostSpearsObj.transform.localScale.y);
            }
            else
            {
                ghostSpearsObj.transform.localScale = new Vector2(1, ghostSpearsObj.transform.localScale.y);

            }
        }
        else
        {
            ghostSpearsObj.transform.localScale = new Vector2(1, ghostSpearsObj.transform.localScale.y);
        }



        if (ghostSpearHit && (chargingSpear || chargingEspecialSpear))
        {
            ghostSpearsObj.transform.position = new Vector2(ghostSpearHit.point.x, ghostSpearHit.point.y);
        }


        RaycastHit2D wallInfo = Physics2D.Raycast(throwPoint.position, Vector2.right * transform.localScale, distanceToThrow, platformLayer);
        RaycastHit2D floorInfo = Physics2D.Raycast(throwPointVerticalUp.position, Vector2.up, distanceToThrow, platformLayer);

        if (Input.GetAxis("Vertical") < 0.2f)
        {
            if (wallInfo)
            {
                ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", false);
            }

            if (!wallInfo)
            {
                ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", true);
            }
        }

        if (ghostSpearsObj.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            if (floorInfo)
            {
                ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", false);
            }

            if (!floorInfo)
            {
                ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", true);
            }
        }


        //if (floorInfo)
        //{
        //    if (Input.GetAxisRaw("Vertical") > 0.2f)
        //    {
        //        ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", false);
        //    }
        //}
        //else
        //{
        //    if (Input.GetAxisRaw("Vertical") > 0.2f)
        //    {
        //        ghostSpearsObj.GetComponent<Animator>().SetBool("canThrow", true);
        //    }
        //}

        //respawn
        if (inDyingTime || inDyingFallingTime)
        {
            timeToRespawn -= Time.deltaTime;
        }

        if (timeToRespawn < 0)
        {
            RestartScene();
        }

    }


    void Update()
    {

        //MANA
        if (mana < maxMana)
        {
            mana += Time.deltaTime /2;
        }
        else
        {
            mana = maxMana;
        }

        CheckHoldLeftTrigger();
        timeToSpawnWalkEffect -= Time.deltaTime;

        //__
        //if (wheelPanel.GetComponent<Animator>() != null)
        //{
            if (wheelPanelActive)
            {
                wheelAnim.SetBool("isActive", true);
            }
            if (!wheelPanelActive)
            {
                wheelAnim.SetBool("isActive", false);
            }
        //}

        //__
        if (!canMove)
        {
            timeToGetControlAfterBounce -= Time.deltaTime;
        }

        if (timeToGetControlAfterBounce <= 0)
        {
            canMove = true;
        }

        if (canControlPlayer)
        {

            //Movimiento


            //Flip escala con el joystick
            if (Input.GetAxis("Horizontal") > 0.3f)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.y);
            }
            else if (Input.GetAxis("Horizontal") < -0.3f)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.y);
            }

            //Salto
            if (isGrounded || isGroundedOther)
            {
                if (rb.velocity.y == 0)
                {
                    jumped = false;
                }
            }

            if (!wheelPanelActive)
            {
                if (isGrounded || isGroundedOther || canLateJump)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
                    {
                        Jump(jumpSpeed);
                    }
                }
            }


            if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Space))
            {
                rb.gravityScale = goingDownGravityScale;
            }

            //EARLY JUMP
            if ((Physics2D.Raycast(groundCheck.position, Vector2.down, earlyJumpDetection, platformLayer)
            || Physics2D.Raycast(groundCheckR.position, Vector2.down, earlyJumpDetection, platformLayer)
            || Physics2D.Raycast(groundCheckL.position, Vector2.down, earlyJumpDetection, platformLayer)) ||
            (Physics2D.Raycast(groundCheck.position, Vector2.down, earlyJumpDetection, walkableLayer)
            || Physics2D.Raycast(groundCheckR.position, Vector2.down, earlyJumpDetection, walkableLayer)
            || Physics2D.Raycast(groundCheckL.position, Vector2.down, earlyJumpDetection, walkableLayer)))
            {
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
                {
                    if (rb.velocity.y < 0)
                    {
                        hasEarlyJumped = true;
                    }
                }
            }

            if (isGrounded || isGroundedOther)
            {
                if (hasEarlyJumped)
                {
                    hasEarlyJumped = false;
                    Jump(jumpSpeed);
                }
            }


            //Lanzar lanzas


            if (!Input.GetButton("Fire1") && !Input.GetButton("Fire2"))
            {
                chargeSpearTime = 0;
                
            }


            if (!isPickingObject)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    hasCancelledThrow = false;
                    isNormalSpear = true;
                }


                if (Input.GetButton("Fire2"))
                {
                    if (!chargingEspecialSpear)
                    {
                        if (chargeSpearTime <= maxSpearCharge)
                        {
                            if (!hasCancelledThrow)
                            {
                                chargeSpearTime += Time.deltaTime;
                                chargingSpear = true;
                            }
                        }
                    }
                }

                if (timeToThrow <= 0)
                {
                    if (Input.GetButtonUp("Fire2"))
                    {
                        if (!chargingEspecialSpear)
                        {
                            ThrowSpear();
                        }

                    }
                }
                else
                {
                    timeToThrow -= Time.deltaTime;
                }

                if (!Input.GetButton("Fire2"))
                {
                    chargingSpear = false;
                }

                if (chargeSpearTime >= maxSpearCharge && !fullChargeEffectMade)
                {
                    isNormalSpear = true;
                    //GameObject fullChargeEffectInstance = Instantiate(chargeEffectParticles, transform.position, Quaternion.identity);
                    //fullChargeEffectInstance.transform.parent = transform;
                    fullChargeEffectMade = true;
                }

                //lanzar lanzasespeciales
                if (Input.GetButtonDown("Fire1"))
                {
                    hasCancelledThrow = false;
                    isNormalSpear = false;
                }


                if (Input.GetButton("Fire1"))
                {
                    if (!chargingSpear)
                    {
                        if ((nextSpearIsBouncy || nextSpearIsBomb || nextSpearIsFire || nextSpearIsStay))
                        {
                            if (chargeSpearTime <= maxSpearCharge)
                            {
                                if (!hasCancelledThrow)
                                {
                                    chargeSpearTime += Time.deltaTime;
                                    chargingEspecialSpear = true;
                                }
                            }
                        }
                    }
                }

                if (timeToThrow <= 0)
                {
                    if (Input.GetButtonUp("Fire1"))
                    {
                        if (chargingEspecialSpear)
                        {
                            if (nextSpearIsBouncy || nextSpearIsBomb || nextSpearIsFire || nextSpearIsStay)
                            {
                                if (mana >= 1)
                                {
                                    mana -= 1;
                                    ThrowSpear();
                                }
                            }
                        }
                    }
                }
                else
                {
                    timeToThrow -= Time.deltaTime;
                }

                if (!Input.GetButton("Fire1"))
                {
                    chargingEspecialSpear = false;
                }

                if (chargeSpearTime >= maxSpearCharge && !fullChargeEffectMade)
                {
                    isNormalSpear = false;
                    //GameObject fullChargeEffectInstance = Instantiate(chargeEffectParticles, transform.position, Quaternion.identity);
                    //fullChargeEffectInstance.transform.parent = transform;
                    fullChargeEffectMade = true;
                }
            }
            



            //if (Input.GetKeyDown(KeyCode.Tab))
            //{
            //    ToggleWheel();
            //}

            if (wheelPanelActive)
            {
                Time.timeScale = 0.02f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            //cancel throw
            if (chargingSpear || chargingEspecialSpear)
            {
                if (Input.GetButtonDown("Fire4"))
                {
                    Instantiate(brokeSpearParticles, transform.position, Quaternion.identity);
                    hasCancelledThrow = true;
                    chargingSpear = false;
                    chargingEspecialSpear = false;
                    //isNormalSpear = true;
                    chargeSpearTime = 0;
                    timeToThrow = 0;

                }
            }

            //destroy last spear on input
            if (spearsCountParent.transform.childCount > 0)
            {
                if (Input.GetButtonDown("Fire4"))
                {
                    DestroyLastSpearAnyways();
                }
            }


            //backspearsssss
            if (spearsCountParent.transform.childCount == 0)
            {
                backSpears[0].SetActive(true);
                backSpears[1].SetActive(true);
                backSpears[2].SetActive(true);
            }

            if (spearsCountParent.transform.childCount == 1)
            {
                backSpears[0].SetActive(false);
                backSpears[1].SetActive(true);
                backSpears[2].SetActive(true);
            }

            if (spearsCountParent.transform.childCount == 2)
            {
                backSpears[0].SetActive(false);
                backSpears[1].SetActive(false);
                backSpears[2].SetActive(true);
            }

            if (spearsCountParent.transform.childCount == 3)
            {
                backSpears[0].SetActive(false);
                backSpears[1].SetActive(false);
                backSpears[2].SetActive(false);
            }

            //Ladders
            RaycastHit2D ladderUpHitInfo = Physics2D.Raycast(throwPointVerticalUp.position, Vector2.up, distanceRayForLadder, ladderLayer);
            RaycastHit2D ladderDownHitInfo = Physics2D.Raycast(groundCheck.position, -Vector2.up, distanceRayForLadder, ladderLayer);

            if (ladderUpHitInfo.collider != null || ladderDownHitInfo.collider != null)
            {
                if (Input.GetAxisRaw("Vertical") == 1 || (Input.GetAxisRaw("Vertical") == -1 && isGrounded))
                {
                    isClimbing = true;
                }
            }
            else
            {
                isClimbing = false;
            }

            if (isClimbing)
            {
                float moveVerticalInput = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, moveVerticalInput * (moveSpeed / 1.5f) * Time.deltaTime);
                rb.gravityScale = 0;
            }
            else
            {

                //Adjusting gravity 
                if (rb.velocity.y > 0 && (Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
                {
                    rb.gravityScale = standardGravityScale;
                }

                if (rb.velocity.y < 0)
                {
                    rb.gravityScale = goingDownGravityScale;
                }
            }

            //PickUpThings
            RaycastHit2D pickableObjHitInfo = Physics2D.Raycast(pickPoint.position, Vector2.right, pickThingDistance, objectLayer);



            if (Input.GetButtonDown("Fire3"))
            {
                if (pickableObjHitInfo)
                {
                    pickedObject = pickableObjHitInfo.collider.transform.parent.gameObject;

                    if (!isPickingObject)
                    {
                        pickedObject.transform.parent = pickPoint.transform;
                        isPickingObject = true;
                        pickedObject.GetComponent<Pickable>().isPicked = true;
                    }
                    else
                    {
                        if (!wallInfoDropObj)
                        {
                            if (Input.GetAxisRaw("Vertical") != -1 && Input.GetAxisRaw("Vertical") != 1)
                            {
                                if (transform.localScale.x > 0)
                                {
                                    if (Input.GetAxisRaw("Horizontal") == 1)
                                    {
                                        pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dropForce / 1.5f, dropForce/2);
                                    }

                                }
                                else if (transform.localScale.x < 0)
                                {
                                    if (Input.GetAxisRaw("Horizontal") == -1)
                                    {
                                        pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dropForce / 1.5f, dropForce/2);
                                    }
                                }
                            }
                            else if (Input.GetAxis("Vertical") == 1)
                            {
                                pickedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, dropForce);
                            }

                            pickedObject.GetComponent<Pickable>().isPicked = false;
                            pickedObject.transform.parent = null;
                            pickedObject = null;
                            isPickingObject = false;
                        }
                    }
                }
            }


            if (Input.GetAxisRaw("VerticalR") != 0)
            {
                movingCameraTimer = 1f;
            }

            if (movingCameraTimer > 0)
            {
                movingCameraTimer -= Time.deltaTime;
                myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
            }
            else
            {
                myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.21f;
            }




            if (Input.GetAxisRaw("VerticalR") == -1)
            {
                if (myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY <= 0.75f)
                {
                    myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
                    myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY += moveCamSpeed * Time.deltaTime;
                }
            }

            if (Input.GetAxisRaw("VerticalR") == 1)
            {
                if (myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY >= 0.25f)
                {
                    myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
                    myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY -= moveCamSpeed * Time.deltaTime;
                }
            }

            if (Input.GetAxis("VerticalR") == 0)
            {

                myVC.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.5f;
            }
        }
        else
        {
            if (!inDyingTime)
            {
                rb.velocity = new Vector2(changingRoomSpeed * Time.deltaTime * transform.localScale.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;
                myCollider.isTrigger = true;
            }
        }

        if (pickedObject == null)
        {
            isPickingObject = false;
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Select"))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            GoToLevelSlector();
        }


    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<EdgeTrigger>() != null)
        {
            //if (Vector2.Distance(transform.position, collision.transform.position) < 0.5f)
            //{
            if (rb.velocity.y <= 0)
            {
                if (Input.GetButton("Jump"))
                {
                    Jump(edgeClimbJumpSpeed);
                }
            }
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explossion>() != null)
        {
            Die();
        }
    }
}



