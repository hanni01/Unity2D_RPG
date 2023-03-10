using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    public float radius;
    public float distance;
    public LayerMask isLayer;
    public LayerMask isGroundLayer;
    float minDepth;
    public Rigidbody2D rb;
    public float jump_speed;
    public GameObject player;
    private bool isground;
    private bool isend;
    Animator aniEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aniEnemy = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    private void Update() {
        aniEnemy.SetBool("isRun", false);
        rb.velocity= Vector3.zero;
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        Collider2D raycastCollider = Physics2D.OverlapCircle(transform.position, radius, isLayer, minDepth);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isGroundLayer);
        if(!aniEnemy.GetBool("isCollided")&& !aniEnemy.GetBool("isDead"))
        {
            if(raycastCollider != null)
            {
                if(transform.position.x < player.GetComponent<Transform>().position.x)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                aniEnemy.SetBool("isRun", true);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if(isend)
                {
                    if(transform.rotation.eulerAngles.y == 180)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        isend = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        isend = false;
                    }
                }
                aniEnemy.SetBool("isRun", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "grass")
        {
            isground = true;
        }
        else
        {
            isground = false;
        }
        if(other.gameObject.tag == "endPoint")
        {
            isend = true;
            Debug.Log("end!!");
        }
    }
}
