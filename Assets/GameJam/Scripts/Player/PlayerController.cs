using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private CircleCollider2D coll;
	private HingeJoint2D hinge;


	[Header("移动设置")]
	public float moveSpeed = 3f;

	[Header("移动状态")]
	public float xVelocity; // x轴的受力方向
	public float yVelocity; // y轴的受力方向

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();  //引用角色刚体
		coll = GetComponent<CircleCollider2D>();
		hinge = GetComponent<HingeJoint2D>();
		hinge.enabled = false;
	}

	
	public void StartPullBodyBall()
	{
		//hinge.enabled = true;
		//var head = GameManager.Instance.bodyBallController.GetHead();
		//var headRig = head.GetComponent<Rigidbody2D>();
		//hinge.connectedBody = headRig;

		var head = GameManager.Instance.bodyBallController.GetHead();
		var headRig = head.GetComponent<Rigidbody2D>();
		headRig.AddForce(((Vector2)transform.position - headRig.position));
	}

	public void EndPullBodyBall()
	{
		//hinge.enabled = false;
		//hinge.connectedBody = null;
	}

	private void Update()
	{
		BallRun();
	}

	void BallRun()
	{
		xVelocity = Input.GetAxis("Horizontal");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
		yVelocity = Input.GetAxis("Vertical");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
		rb.velocity = new Vector2(xVelocity * moveSpeed, yVelocity * moveSpeed);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartPullBodyBall();
			// 播放音效
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			EndPullBodyBall();
			// 播放音效
		}


	}


	

}
