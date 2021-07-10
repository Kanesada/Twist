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
		if (isTail)
		{ 
			hingeJoint2D.enabled = false;
			Deactive();
		}
		else
		{
			hingeJoint2D.enabled = true;
			Active();
		}
		hingeJoint2D.connectedBody = tailRig;
	}

	public void SetNext(Rigidbody2D nextRig)
	{
		hingeJoint2D.enabled = true;
		hingeJoint2D.connectedBody = nextRig;
	}

	public void Active(BallType type = BallType.None)
	{
		spriteRender.enabled = true;
		gameObject.layer = LayerMask.NameToLayer("BodyBall");
	}

	public void Deactive()
	{
		spriteRender.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("VirtualBall");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ͷ���Ե�����
		if (collision.tag == "ItemBall" && isHead == true)
		{
			var ballType = collision.GetComponent<ItemBall>().ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddBodyBall(ballType);
		}

		// ͷ��������������
		if (collision.tag == "Traps")
		{
			GameManager.Instance.RemoveBodyBall(this);
			// ������Ч
		}
		

	}
}
