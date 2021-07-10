using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBall : MonoBehaviour
{
	public BallType ballType;

	public bool isHead;

	public SpriteRenderer spriteRender;

	private Animator anim;
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

	public void Active(BallType type)
	{
		hingeJoint2D.enabled = true;
		spriteRender.enabled = true;
		gameObject.layer = LayerMask.NameToLayer("BodyBall");
        switch (type)
		{
			case BallType.Love:
				gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("love",typeof(Sprite)) as Sprite;
				break;
			case BallType.Money:
				gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("money", typeof(Sprite)) as Sprite;
				//gameObject.GetComponentInChildren<Animator>().SetBool("money", true);
				break;
			case BallType.Virus:
				gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("virus", typeof(Sprite)) as Sprite;
				break;
			case BallType.Smoke:
				gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("smoke", typeof(Sprite)) as Sprite;
				break;
			case BallType.Book:
				gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("book", typeof(Sprite)) as Sprite;
				break;
			default:
				break;
			
		}










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
		else if (collision.tag == "Traps") // 头或者身碰到陷阱
		{
			GameManager.Instance.RemoveBodyBall(this);
			//减少生命
			GameObject.Find("Canvas").GetComponent<UIManager>().LoseLife();
		}
		else if (collision.tag == "WinZone" && isHead == true) // 头碰到关卡出口
		{
			// 播放音效
		}
		else if (collision.tag == "Obstacle") // 碰到障碍物
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
