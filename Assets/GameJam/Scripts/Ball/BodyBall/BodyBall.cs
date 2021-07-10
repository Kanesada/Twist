using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBall : MonoBehaviour
{
	public BallType ballType;

	public bool isHead;

	public SpriteRenderer spriteRender;


	public HingeJoint2D hingeJoint2D;

	public void Init(bool isHead, Rigidbody2D tailRig, bool isTail = false)
	{
		this.isHead = isHead;
		hingeJoint2D = GetComponent<HingeJoint2D>();
		hingeJoint2D.connectedBody = tailRig;
		Deactive();
	}

	public void SetNext(Rigidbody2D nextRig)
	{
		hingeJoint2D.enabled = true;
		hingeJoint2D.connectedBody = nextRig;
	}

	public void Active(BallType type = BallType.None)
	{
		hingeJoint2D.enabled = true;
		spriteRender.enabled = true;
		gameObject.layer = LayerMask.NameToLayer("BodyBall");
	}

	public void Deactive()
	{
		hingeJoint2D.enabled = false;
		spriteRender.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("VirtualBall");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "ItemBall" && isHead == true) // 头部吃到道具
		{
			var ballType = collision.GetComponent<ItemBall>().ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddBodyBall(ballType);
		}
		


	}

	private void nCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Traps") // 头或者身碰到陷阱
		{
			GameManager.Instance.RemoveBodyBall(this);
		}

		else if (collision.gameObject.tag == "Obstacle") // 碰到障碍物
		{
			// 播放音效
		}
	}




	private void Update()
	{
		float radius = 1f;
		var pos = transform.position;
		Debug.DrawLine(pos, pos + Vector3.one);
	}

}
