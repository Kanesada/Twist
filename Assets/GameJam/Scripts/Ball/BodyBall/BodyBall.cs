using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBall : MonoBehaviour
{
	public BallType ballType;

	public bool isHead;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "ItemBall" && isHead == true)
		{
			var ballType = collision.GetComponent<ItemBall>().ballType;
			Destroy(collision.gameObject);

			GameManager.Instance.AddBodyBall(ballType);
		}
	}
}
