using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;
	private CircleCollider2D coll;


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
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void FixedUpdate()
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
			
		}
	}


	

}
