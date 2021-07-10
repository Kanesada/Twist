using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBallController
{

	private Rigidbody2D tailRig;

	private List<BodyBall> bodyBallList = new List<BodyBall>();

	public void GenerateBody(int maxCount)
	{
		var bodyTypeList = GameManager.Instance.gamerData.bodyBallList;
		var bodyballprefab = GameManager.Instance.bodyBallPrefab;

		var tail = GameObject.Instantiate(bodyballprefab);
		tailRig = tail.GetComponent<Rigidbody2D>();
		var hingeJoint2D = tailRig.GetComponent<HingeJoint2D>();
		hingeJoint2D.enabled = false;
		hingeJoint2D.connectedBody = null;

		// 生成虚拟球串
		for (int i = 0; i < maxCount; i++)
		{
			var bodyballGO = GameObject.Instantiate(bodyballprefab);
			var bodyBall = bodyballGO.GetComponent<BodyBall>();
			var bodyBallRig = bodyballGO.GetComponent<Rigidbody2D>();

			if (i == 0)
			{
				bodyBall.Init(true, tailRig);
			}
			else if (i > 0)
			{
				bodyBall.Init(false, tailRig);

				var lastbodyball = bodyBallList[bodyBallList.Count - 1];
				lastbodyball.SetNext(bodyBallRig);
			}
			
			bodyBallList.Add(bodyBall);
		}

		// 按数据生成真实球串
		
	}

	private BodyBall GenerateBodyBallFactory(BallType ballType)
	{
		return null;
	}
}
