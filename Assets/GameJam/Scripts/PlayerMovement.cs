using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    private Vector2 direction;
    private Rigidbody2D ballRb;
    private Animator anim;
    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

    [Header("Header 球")]
    public GameObject headerBall;

    

    [Header("移动设置")]
    public float moveSpeed = 10f;

    [Header("移动状态")]
    public float xVelocity; // x轴的受力方向
    public float yVelocity; // y轴的受力方向

    [Header("角色拉力")]
    public bool pullPressed = false;  // 角色是否按下拉
    public float pullForce = 10f;  //角色拉力

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //引用角色刚体
        coll = GetComponent<CircleCollider2D>();  //角色碰撞
        ballRb = headerBall.GetComponent<Rigidbody2D>();  //获取header球的刚体
        anim = GetComponent<Animator>();

        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pullPressed = true;
            anim.SetBool("pull", true);
        }
        if(Input.GetButtonUp("Jump")) anim.SetBool("pull", false);


        if (Input.GetKeyDown(KeyCode.E))
        {
            MyInpulse.GenerateImpulse();
            print(11111);
        }


        
    }
    private void FixedUpdate()
    {
        PlayerRun();
        PlayerPull();
        FlipDirection();
    }

    void PlayerRun()
    {
        xVelocity = Input.GetAxis("Horizontal");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        yVelocity = Input.GetAxis("Vertical");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        rb.velocity = new Vector2(xVelocity * moveSpeed, yVelocity * moveSpeed);

        if(xVelocity != 0 || yVelocity != 0)
        {
            anim.SetInteger("smile", 10);
            if (xVelocity != 0)
            {
                anim.SetFloat("running", Mathf.Abs(xVelocity));
            }
            else anim.SetFloat("running", Mathf.Abs(yVelocity));
        }
        else
        {
            //静止时彩蛋
            int a = Random.Range(0, 10);
            anim.SetInteger("smile", a);
        }
    }

    
	void PlayerPull()
    {
        if(pullPressed == true)  // 当按下空格
        {
			if (headerBall == null)
			{
				headerBall = GameManager.Instance.bodyBallController.GetHead();
				ballRb = headerBall.GetComponent<Rigidbody2D>();
			}
			if (ballRb == null)
			{
				ballRb = headerBall.GetComponent<Rigidbody2D>();
			}

            direction = transform.position - headerBall.transform.position;  // 获取角色与header球的方向
            direction = direction.normalized;  // 方向向量单位化
            ballRb.AddForce(direction * pullForce,ForceMode2D.Impulse);
            pullPressed = false;
            
            
        }

		// 播放音效
	}


	void FlipDirection()  //控制角色行动时贴图的方向
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (xVelocity > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


  



}
