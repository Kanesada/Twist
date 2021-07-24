using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBallController : MonoBehaviour
{
	public GameObject prefab;

	public SpringBodyBall head;

	public float SpringK = 1.0f;
	public float AirResistanceRatio = 0.1f;
	[Range(0, 1)]
	public float Damping = 0.1f;
	public float Space = 0.5f;

	private List<SpringBodyBall> springBalls = new List<SpringBodyBall>();


	public void Start()
	{
		GameManager.Instance.springBallController = this;

		springBalls.Add(head);
	}

	public void AddSpringBall(BallType type)
	{
		var lastBall = springBalls[springBalls.Count - 1];
		var go = GameObject.Instantiate(prefab, lastBall.transform.position + new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);
		go.transform.SetParent(gameObject.transform);

		var springBall = go.GetComponent<SpringBodyBall>();
		springBall.Active(type);

		springBalls.Add(springBall);
	}


	public void RemoveSpringBall(SpringBodyBall spring)
	{
		int i = 0;
		while(i < springBalls.Count)
		{
			var springBall = springBalls[i];
			if (springBall != spring)
			{
				i++;
				continue;
			}

			while (i < springBalls.Count)
			{
				Destroy(springBalls[i].gameObject);
				springBalls.RemoveAt(i);
			}
		}

	}

	private void Update()
	{
		for (int i = 1; i < springBalls.Count; i++)
		{
			var springBall = springBalls[i];
			var preSpringBall = springBalls[i - 1];
			Vector3 forceDir = preSpringBall.transform.position - springBall.transform.position;
			Vector3 springForce = SpringK * forceDir.normalized * (forceDir.magnitude - Space);

			if (i < springBalls.Count - 1)
			{
				forceDir = springBalls[i + 1].transform.position - springBall.transform.position;
				//springForce += SpringK * forceDir.normalized * (forceDir.magnitude - Space);
			}

			springBall.rig.AddForce((Vector2)springForce - AirResistanceRatio * springBall.rig.velocity.magnitude * springBall.rig.velocity);
		}
	}


	public int GetBodyBallIndex(SpringBodyBall spring)
	{
		return springBalls.FindIndex((SpringBodyBall bb) => bb == spring);
	}
}
