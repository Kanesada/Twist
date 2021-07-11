using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBall : MonoBehaviour
{
	public LineRenderer lineRenderer;

	public BallType ballType;

	public bool isHead;

	public SpriteRenderer spriteRender;

	private Animator anim;
	public HingeJoint2D hingeJoint2D;

	public BodyBall nextBodyBall;

	public bool isBodyBall;

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
		nextBodyBall = nextRig.GetComponent<BodyBall>();
	}

	public void Active(BallType type)
	{
		isBodyBall = true;
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
		isBodyBall = false;
		hingeJoint2D.enabled = false;
		spriteRender.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("VirtualBall");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isBodyBall == false)
			return;

		if (collision.tag == "ItemBall" && isHead == true) // ͷ���Ե�����
		{
			var ballType = collision.GetComponent<ItemBall>().ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddBodyBall(ballType);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isBodyBall == false)
			return;

		if (collision.gameObject.tag == "Traps") // ͷ��������������
		{
			GameManager.Instance.RemoveBodyBall(this);
			//减少生命
			GameObject.Find("Canvas").GetComponent<UIManager>().LoseLife();
		}

		else if (collision.gameObject.tag == "Obstacle") // �����ϰ���
		{
			// ������Ч
		}
	}




	private void Update()
	{
		var pos = transform.position;
		Debug.DrawLine(pos, pos + Vector3.one);


		if (lineRenderer != null && lineRenderer.enabled == true && nextBodyBall != null && nextBodyBall.isBodyBall == true)
		{ 
			List<Vector3> points = new List<Vector3>();
			var pos1 = this.transform.position;
			pos1.z = -1;
			points.Add(pos1);
			var pos2 = nextBodyBall.transform.position;
			pos2.z = -1;
			points.Add(pos2);

			lineRenderer.SetPositions(points.ToArray());

			lineRenderer.startWidth = 0.1f;
			lineRenderer.endWidth = 0.1f;
		}
	}

}
