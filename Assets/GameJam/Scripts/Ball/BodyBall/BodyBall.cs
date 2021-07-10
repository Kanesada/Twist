using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBall : MonoBehaviour
{
	public BallType ballType;

	public bool isHead;

	public SpriteRenderer spriteRender;


	public HingeJoint2D hingeJoint2D;

	public void Init(bool isHead, Rigidbody2D tailRig)
	{
		this.isHead = isHead;
		hingeJoint2D = GetComponent<HingeJoint2D>();
		hingeJoint2D.enabled = true;
		hingeJoint2D.connectedBody = tailRig;
	}

	public void SetNext(Rigidbody2D nextRig)
	{
		hingeJoint2D.enabled = true;
		hingeJoint2D.connectedBody = nextRig;
	}

	public void Active(BallType type)
	{
		spriteRender.enabled = true;
	}

	public void Deactive()
	{
		spriteRender.enabled = false;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "ItemBall" && isHead == true)
		{
			var ballType = collision.GetComponent<ItemBall>().ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddBodyBall(ballType);
		}

		if (collision.tag == "Traps")
		{
			
		}
	}
}
