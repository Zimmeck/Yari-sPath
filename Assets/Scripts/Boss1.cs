using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss1 : MonoBehaviour
{
    public GameObject[] bossTurrets;
    public GameObject[] bossSpikes;
    public GameObject darkSpearPrefabR;
    public GameObject darkSpearPrefabL;
    public Transform shootPoint;
    private GameObject playerRef;
    public GameObject bossSpearsParent;
    public float aimSpeed;
    private Vector2 newPos;
    public GameObject handsSpearDestroyObj;
    public GameObject bossCam;
    public GameObject bossCam2;
    public GameObject bossCam3;
    private Animator myGrowAnim;
    public SpriteRenderer headSprite;
    public GameObject angryFaceSprite;
    public GameObject relaxFaceSprite;
    public GameObject maskSprite;
    public LineRenderer aimLine;
    private RaycastHit2D lineEnd;
    public LayerMask platformLayer;
    private bool playerIsRight;
    public GameObject shootEffect;

    public float startTimeToShoot;
    public float timeToShoot;
    public float health;
    private bool isInmune;
    public bool isChangingStage;
    private bool canAim = true;
    private bool inCoroutine = false;
    public bool activated;
    private bool shooting;

    public float shootingEffectTime;
    public GameObject bossGateEnter;
    public GameObject bossGateExit;

    private void DesactivarCosasAlAcabar()
    {
        for (int i = 0; i < bossTurrets.Length; i++)
        {
            bossTurrets[i].GetComponent<Turret>().enabled = false;
        }

        for (int i = 0; i < bossSpikes.Length; i++)
        {
            Destroy(bossSpikes[i].GetComponent<Spikes>().gameObject);
        }

        for (int i = 0; i < FindObjectsOfType<TurretBullet>().Length; i++)
        {
             Destroy(FindObjectsOfType<TurretBullet>()[i].gameObject);
        }

        bossGateExit.GetComponent<Animator>().SetBool("Open", true);
        bossGateEnter.GetComponent<Animator>().SetBool("closed", false);

        aimLine.enabled = false;

        FindObjectOfType<CameraBehaviour>().ActivateFollowCam();
        bossCam.SetActive(false);
        bossCam2.SetActive(false);
        bossCam3.SetActive(false);

    }

    private void ShootSpear()
    {
        if (!isInmune)
        {

            if (playerIsRight)
            {
                GameObject newSpear = Instantiate(darkSpearPrefabR, shootPoint.transform.position, Quaternion.identity);
                newSpear.transform.parent = bossSpearsParent.transform;
                if (health == 1)
                {
                    newSpear.GetComponent<SpearScr>().isBombSpear = true;
                }
            }
            else
            {
                GameObject newSpear = Instantiate(darkSpearPrefabL, shootPoint.transform.position, Quaternion.identity);
                newSpear.transform.parent = bossSpearsParent.transform;
                if (health == 1)
                {
                    newSpear.GetComponent<SpearScr>().isBombSpear = true;
                }
            }

            timeToShoot = startTimeToShoot;
            aimLine.widthMultiplier = 1;

            if (bossSpearsParent.transform.childCount > 3)
            {
                bossSpearsParent.transform.GetChild(0).GetComponent<SpearScr>().DestroySpear();
            }

            Instantiate(shootEffect, shootPoint.transform.position, Quaternion.identity);
        }
    }

    public void DecreaseHealth()
    {
        if (!isInmune)

        {
            Camera.main.gameObject.GetComponent<CameraBehaviour>().DoCameraShake();
            aimLine.widthMultiplier = 1;

            health -= 1;
            for (int i = 0; i < bossSpearsParent.transform.childCount; i++)
            {
                bossSpearsParent.transform.GetChild(i).GetComponent<SpearScr>().DestroySpear();
            }

            if (health == 2)
            {
                myGrowAnim.SetInteger("Fase", 1);
                bossCam.SetActive(false);
                bossCam2.SetActive(true);
                Camera.main.gameObject.GetComponent<CameraBehaviour>().actualCamera = bossCam2.GetComponent<CinemachineVirtualCamera>();
            }
            else if (health == 1)
            {
                myGrowAnim.SetInteger("Fase", 2);
                aimSpeed = aimSpeed * 2;
                //startTimeToShoot = startTimeToShoot / 2;
                bossCam2.SetActive(false);
                bossCam3.SetActive(true);

                for (int i = 0; i < bossTurrets.Length; i++)
                {
                    bossTurrets[i].GetComponent<Turret>().enabled = true;
                }
                Camera.main.gameObject.GetComponent<CameraBehaviour>().actualCamera = bossCam3.GetComponent<CinemachineVirtualCamera>();

            }
            else if (health == 0)
            {
                activated = false;
                DesactivarCosasAlAcabar();
            }
            Camera.main.gameObject.GetComponent<CameraBehaviour>().DoCameraShake();
            Camera.main.gameObject.GetComponent<CameraBehaviour>().DoLongCameraShake();
            StartCoroutine(HurtEffect());
            StartCoroutine(InmuneTime());
        }
    }

    private IEnumerator ShootRafaga()
    {
        shooting = true;
        ShootSpear();
        yield return new WaitForSeconds(1f);
        ShootSpear();
        yield return new WaitForSeconds(1f);
        ShootSpear();
        shooting = false;
    }

    private IEnumerator ShootRafagaFast()
    {
        shooting = true;
        ShootSpear();
        yield return new WaitForSeconds(.5f);
        ShootSpear();
        yield return new WaitForSeconds(.5f);
        ShootSpear();
        shooting = false;
    }

    private IEnumerator StopAndShoot()
    {
        canAim = false;
        inCoroutine = true;
        //aimLine.widthMultiplier = .3f;



        if (playerRef.transform.position.x > shootPoint.transform.position.x)
        {
            playerIsRight = true;
        }
        else
        {
            playerIsRight = false;
        }
        yield return new WaitForSeconds(.7f);
        if (!isInmune)
        {
            int randNum = Random.Range(0, 2);

            if (health == 3)
            {
                ShootSpear();
            }
            else if (health <= 2)
            {
                StartCoroutine(ShootRafaga());
            }
        }

        canAim = true;
        inCoroutine = false;

    }

    private IEnumerator InmuneTime()
    {
        isInmune = true;
        relaxFaceSprite.SetActive(false);
        angryFaceSprite.SetActive(true);
        handsSpearDestroyObj.SetActive(false);
        yield return new WaitForSeconds(3f);
        isInmune = false;
        handsSpearDestroyObj.SetActive(true);
        relaxFaceSprite.SetActive(true);
        angryFaceSprite.SetActive(false);
    }

    private IEnumerator HurtEffect()
    {
        headSprite.color = Color.red;
        yield return new WaitForSeconds(.05f);
        headSprite.color = Color.white;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerRef = FindObjectOfType<PlayerScr>().gameObject;
        myGrowAnim = GetComponent<Animator>();
        timeToShoot = startTimeToShoot;
    }


    // Update is called once per frame
    void Update()
    {
        //BORRARRRRRRR
        if (Input.GetKeyDown(KeyCode.H))
        {
            DecreaseHealth();
        }


        //BORRARRRRRRRR

        if (activated)
        {
            //aimLine.SetPosition(1, new Vector3(aimLine.GetPosition(0).x, shootPoint.transform.position.y, aimLine.GetPosition(0).z));
            if (inCoroutine)
            {
                if (aimLine.widthMultiplier > 0.2f)
                {
                    aimLine.widthMultiplier -= 1.5f * Time.deltaTime;
                }
            }

            if (playerRef.transform.position.x > transform.position.x)
            {
                lineEnd = Physics2D.Raycast(shootPoint.transform.position, shootPoint.transform.right * transform.localScale.x, 1000000, platformLayer);
            }
            else if (playerRef.transform.position.x < transform.position.x)
            {
                lineEnd = Physics2D.Raycast(shootPoint.transform.position, -shootPoint.transform.right * transform.localScale.x, 1000000, platformLayer);
            }


            if (!shooting)
            {
                if (playerIsRight)
                {
                    aimLine.SetPosition(1, new Vector3(lineEnd.point.x, shootPoint.transform.position.y, transform.position.z));
                }
                else if (!playerIsRight)
                {
                    aimLine.SetPosition(1, new Vector3(lineEnd.point.x, shootPoint.transform.position.y, transform.position.z));
                }
            }
            else
            {
                aimLine.SetPosition(1, new Vector3(lineEnd.point.x, shootPoint.transform.position.y, aimLine.GetPosition(0).z));
                aimLine.widthMultiplier = .2f;
            }

            if (!isInmune)
            {
                aimLine.enabled = true;
                if (playerRef.transform.position.x < transform.position.x)
                {
                    if (health > 1)
                    {
                        newPos = new Vector2(transform.position.x - 2.5f, playerRef.transform.position.y);
                    }
                    else
                    {
                        newPos = new Vector2(transform.position.x - 6f, playerRef.transform.position.y);
                    }

                }

                if (playerRef.transform.position.x > transform.position.x)
                {

                    if (health > 1)
                    {
                        newPos = new Vector2(transform.localPosition.x + 2.5f, playerRef.transform.position.y);
                    }
                    else
                    {
                        newPos = new Vector2(transform.localPosition.x + 6f, playerRef.transform.position.y);
                    }

                }

                if (canAim)
                {
                    //if (health == 3)
                    //{
                    //    if (shootPoint.transform.localPosition.y <= 2f)
                    //    {
                    //        shootPoint.transform.position = Vector2.MoveTowards(shootPoint.transform.position, newPos, aimSpeed * Time.deltaTime);
                    //    }
                    //    else
                    //    {
                    //        shootPoint.transform.localPosition = new Vector2(shootPoint.transform.localPosition.x, 2f);
                    //    }
                    //}
                    shootPoint.transform.position = Vector2.MoveTowards(shootPoint.transform.position, newPos, aimSpeed * Time.deltaTime);
                    if (health == 1)
                    {
                        if (maskSprite != null)
                        {
                            maskSprite.SetActive(true);
                        }
                    }

                    aimLine.SetPosition(0, shootPoint.transform.position);

                    

                }

                if (timeToShoot <= 0)
                {
                    if (!inCoroutine)
                    {
                        StartCoroutine(StopAndShoot());
                    }
                }
                else
                {
                    timeToShoot -= Time.deltaTime;
                }
            }
            else
            {
                aimLine.enabled = false;
            }
        }  
    }
}
