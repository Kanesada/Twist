using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBodyBall : MonoBehaviour
{
	public BallType ballType;

	public bool isHead;

	public bool isBodyBall;

	public SpriteRenderer spriteRender;

	public Rigidbody2D rig;

	public float radius;

	public void Active(BallType type)
	{
		switch (type)
		{
			case BallType.Love:
				spriteRender.sprite = Resources.Load("love", typeof(Sprite)) as Sprite;
				break;
			case BallType.Money:
				spriteRender.sprite = Resources.Load("money", typeof(Sprite)) as Sprite;
				break;
			case BallType.Virus:
				spriteRender.sprite = Resources.Load("virus", typeof(Sprite)) as Sprite;
				break;
			case BallType.Smoke:
				spriteRender.sprite = Resources.Load("smoke", typeof(Sprite)) as Sprite;
				break;
			case BallType.Book:
				spriteRender.sprite = Resources.Load("book", typeof(Sprite)) as Sprite;
				break;
			default:
				break;
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isBodyBall == false)
			return;

		if (collision.tag == "ItemBall" && isHead== true)
		{
			var itemBall = collision.GetComponent<ItemBall>();
			var type = itemBall.ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddSpringBodyBall(type);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isBodyBall == false)
			return;

		if (collision.gameObject.tag == "Traps")
		{
			GameManager.Instance.RemoveSpringBodyBall(this);
			//减少生命
			GameObject.Find("Canvas")?.GetComponent<UIManager>().LoseLife();
		}
	}
}
