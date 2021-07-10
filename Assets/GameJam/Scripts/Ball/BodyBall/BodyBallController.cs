using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBallController
{

	private Rigidbody2D tailRig;

	private List<BodyBall> bodyBallList = new List<BodyBall>();

	private int maxCount;

	public void GenerateBody(int maxCount)
	{
		foreach(var bodyBall in bodyBallList)
		{
			GameObject.Destroy(bodyBall.gameObject);
		}
		bodyBallList.Clear();

		this.maxCount = maxCount;

		var bodyTypeList = GameManager.Instance.gamerData.bodyBallList;
		var headballprefab = GameManager.Instance.headBallPrefab;
		var bodyballprefab = GameManager.Instance.bodyBallPrefab;

		if (bodyTypeList.Count > maxCount)
		{
			throw new System.Exception("maxCount 大点");
		}

		var tail = GameObject.Instantiate(bodyballprefab);
		tailRig = tail.GetComponent<Rigidbody2D>();
		var hingeJoint2D = tailRig.GetComponent<HingeJoint2D>();
		hingeJoint2D.enabled = false;
		hingeJoint2D.connectedBody = null;
		tail.GetComponent<BodyBall>().Init(false, null, true);

		// 生成虚拟球串
		for (int i = 0; i < maxCount; i++)
		{
			BodyBall bodyBall = null;
			if (i == 0)
			{
				var headBallGo = GameObject.Instantiate(headballprefab);
				bodyBall = headBallGo.GetComponent<BodyBall>();
				var headBallRig = headBallGo.GetComponent<Rigidbody2D>();

				bodyBall.Init(true, tailRig);
			}
			else if (i > 0)
			{
				var bodyballGO = GameObject.Instantiate(bodyballprefab);
				bodyBall = bodyballGO.GetComponent<BodyBall>();
				var bodyBallRig = bodyballGO.GetComponent<Rigidbody2D>();

				bodyBall.Init(false, tailRig);

				var lastbodyball = bodyBallList[bodyBallList.Count - 1];
				lastbodyball.SetNext(bodyBallRig);
			}
			
			bodyBallList.Add(bodyBall);
		}


		ChangeBodyBallVisiable();
	}

	public void ChangeBodyBallVisiable()
	{
		var bodyTypeList = GameManager.Instance.gamerData.bodyBallList;

		for (int i = 0; i < bodyBallList.Count; i++)
		{
			var bodyBall = bodyBallList[i];
			if (i < bodyTypeList.Count)
			{
				var type = bodyTypeList[i];
				bodyBall.Active(type);
			}
			else
			{
				bodyBall.Deactive();
			}
		}
	}

	public int GetBodyBallIndex(BodyBall bodyBall)
	{
		return bodyBallList.FindIndex((BodyBall bb) => bb == bodyBall);
	}

	public GameObject GetHead()
	{
		return bodyBallList[0].gameObject;
	}

}
