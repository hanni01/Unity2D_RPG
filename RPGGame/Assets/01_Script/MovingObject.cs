using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //캐릭터의 속도 선언
    public float speed;
    public float runSpeed;
    private float applyRunSpeed;

    private bool applyRunFlag = false;
    private bool isGround = true;
    public Rigidbody2D rb;
    public float jump_speed;

    private Animator animator;
    public GameObject bolt1;
    public Transform bolt_position;
    SpriteRenderer spriteRenderer;

    int playerLayer, groundLayer;
    float moveX;
    AudioSource audioSource;
    public AudioClip jump;
    public AudioClip boltAttack;

    public Animator aniSuccessAttack;

    public bool isAttacked = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bolt_position = this.transform.Find("bolt1Position");

        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("platform");

        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal") * (speed+applyRunSpeed) * Time.deltaTime;
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        if(moveX != 0)
        {
            animator.SetBool("isRun", true);
        }else
        {
            animator.SetBool("isRun", false);
        }

        if(isGround == true && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            isGround = false;

            audioSource.clip = jump;
            audioSource.Play();

            rb.AddForce(Vector3.up*jump_speed, ForceMode2D.Impulse);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRunFlag = true;
        }
        else{
            applyRunSpeed = 0;
            applyRunFlag = false;
        }

        //왼쪽 오른쪽 달릴 때 캐릭터 방향 전환
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        //점프할 때 땅과의 레이어 충돌 무시
        if(rb.velocity.y >= 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
        }else{
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if(transform.localScale.x == 0.5f)
            {
                this.bolt1.transform.localScale = new Vector3(4, 4, 2);
            }
            else if(transform.localScale.x == -0.5f)
            {
                this.bolt1.transform.localScale = new Vector3(-4, 4, 2);
            }

            var boltClone = Instantiate<GameObject>(this.bolt1);
            boltClone.transform.position = this.bolt_position.position;
            boltClone.SetActive(true);
            if(boltClone != null)
            {
                Destroy(boltClone, 2.0f);
                Debug.Log("time to disappear");
            }

            audioSource.clip = boltAttack;
            audioSource.Play();
        }
        
    }

    private void disappearBall(GameObject obj)
    {
        if(aniSuccessAttack.GetBool("isCollided"))
        {
            Debug.Log("collided");
            Destroy(obj);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground")
        {
            animator.SetBool("isJump", false);
            isGround = true;
        }
        if (other.gameObject.layer == 9)
        {
            isAttacked = true;
            rb.AddForce(Vector3.up * 7, ForceMode2D.Impulse);
        }
    }
}
