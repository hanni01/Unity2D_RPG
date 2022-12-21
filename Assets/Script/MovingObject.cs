using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //캐릭터의 속도 선언
    public float speed;
    //3개의 값을 동시에 가지고 있는 변수 선언
    private Vector3 vector;
    public float runSpeed;
    private float applyRunSpeed;

    private int jumpCnt;

    public int walkCount;
    private int currentWalkCount;
    private bool canMove = true;
    private bool applyRunFlag = false;
    private bool isGround = true;
    public Rigidbody2D rb;
    public float jump_speed;

    private Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Horizontal") != 0)
        {
            //쉬프트키 누르면 달리기
            if(Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else{
                applyRunSpeed = 0;
                applyRunFlag = false;
            }
            //이 스크립트에 적용되는 객체의 z축의 좌표 값을 계속 z에 넣어준다(어차피 변하지 않음)
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            
            if(vector.x != 0)
            {
                animator.SetBool("isRun", true);
            }
            
            //왼쪽 오른쪽 달릴 때 캐릭터 방향 전환
            if(Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }else if(Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            // speed = 0.02, walkCount = 20
            // 0.02 * 20 = 0.4, 0.4 pixels 만큼 움직이게 하겠다
            while(currentWalkCount < walkCount)
            {
                if(vector.x != 0)
                {
                    //Translate() -> 현재 값에서 매개변수로 넘어온 값만큼 수치를 더한다.
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                if(applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("isRun", false);
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            //Axis->화살키, Horizontal = 수평 = 우 방향키 눌리면 1리턴, 좌 방향키 -1리턴
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine()); 
            }
        }
        if(isGround == true && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            isGround = false;
            rb.AddForce(Vector3.up*jump_speed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
            animator.SetBool("isJump", false);
            isGround = true;
        }
}
