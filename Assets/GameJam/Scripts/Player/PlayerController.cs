using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private CircleCollider2D coll;
	private HingeJoint2D hinge;


	[Header("�ƶ�����")]
	public float moveSpeed = 3f;

	[Header("�ƶ�״̬")]
	public float xVelocity; // x�����������
	public float yVelocity; // y�����������

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();  //���ý�ɫ����
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
		xVelocity = Input.GetAxis("Horizontal");  //��ȡˮƽ�����ƶ�ָ�-1f~1f ������ʱ���Զ���0 ��˲�����ֻ���
		yVelocity = Input.GetAxis("Vertical");  //��ȡˮƽ�����ƶ�ָ�-1f~1f ������ʱ���Զ���0 ��˲�����ֻ���
		rb.velocity = new Vector2(xVelocity * moveSpeed, yVelocity * moveSpeed);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartPullBodyBall();
			// ������Ч
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			EndPullBodyBall();
			// ������Ч
		}


	}


	

}
