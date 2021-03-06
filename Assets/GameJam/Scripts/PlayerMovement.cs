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
	public Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

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

	[Header("绳子特效")]
	public LineRenderer lineRenderer;

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

		RenderString();

    }
    private void FixedUpdate()
    {
        PlayerRun();
        PlayerPull();
        FlipDirection();
    }

	void RenderString()
	{
		List<Vector3> points = new List<Vector3>();
		var pos1 = this.transform.position - 0.5f * Vector3.up;
		pos1.z = -1;
		points.Add(pos1);
		var pos2 = headerBall.transform.position;
		pos2.z = -1;
		points.Add(pos2);

		lineRenderer.SetPositions(points.ToArray());

		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
	}

    void PlayerRun()
    {
        xVelocity = Input.GetAxis("Horizontal");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        yVelocity = Input.GetAxis("Vertical");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        rb.velocity = new Vector2(xVelocity * moveSpeed, yVelocity * moveSpeed);
        if (rb.velocity.x != 0 || rb.velocity.y != 0) AudioManager.PlayFootstepAudio(); //移动时播放脚步声

        if((rb.velocity.x != 0f) || (rb.velocity.y != 0f))
        {
            if (xVelocity != 0)
            {
                anim.SetFloat("running", Mathf.Abs(xVelocity));
            }
            else anim.SetFloat("running", Mathf.Abs(yVelocity));
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
            AudioManager.PlayPullAudio();  //播放拉音效
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

	public void CameraShake()
	{
		MyInpulse.GenerateImpulse();
	}




}
