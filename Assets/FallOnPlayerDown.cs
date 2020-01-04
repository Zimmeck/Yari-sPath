using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnPlayerDown : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform rayPoint;
    public float detectionDistance;

    private IEnumerator WaitAndFall()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
    }

    private void Fall()
    {
        rb.isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.down, detectionDistance);
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Fall();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPoint.position, Vector2.down * detectionDistance);
    }
}
