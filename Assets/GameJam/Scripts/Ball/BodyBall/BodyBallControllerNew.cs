using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BodyBallControllerNew : MonoBehaviour
{

	public Transform circleList;

	public BodyBall head;

	private Rigidbody2D tailRig;

	private List<BodyBall> bodyBallList = new List<BodyBall>();

	private int maxCount;


	private void Start()
	{
		GenerateBody(5);
		GameManager.Instance.bodyBallController = this;
	}


	public void GenerateBody(int maxCount)
	{
		bodyBallList.Clear();

		this.maxCount = maxCount;

		var bodyTypeList = GameManager.Instance.gamerData.BodyBallList;
		if (bodyTypeList.Count > maxCount)
		{
			maxCount = bodyTypeList.Count;
		}

		var bodyballprefab = GameManager.Instance.bodyBallPrefab;

		var tail = GameObject.Instantiate(bodyballprefab);
		tailRig = tail.GetComponent<Rigidbody2D>();
		var hingeJoint2D = tailRig.GetComponent<HingeJoint2D>();
		hingeJoint2D.enabled = false;
		hingeJoint2D.connectedBody = null;
		tail.GetComponent<BodyBall>().Init(false, null, true);
		tail.transform.SetParent(circleList);
		tail.name = "Tail";

		head.Init(true, tailRig);
		bodyBallList.Add(head);

		// Éú³ÉÐéÄâÇò´®
		for (int i = 1; i < maxCount; i++)
		{
			BodyBall bodyBall = null;
			if (i > 0)
			{
				var bodyballGO = GameObject.Instantiate(bodyballprefab, transform.position + 2 * Vector3.one ,Quaternion.identity);
				bodyBall = bodyballGO.GetComponent<BodyBall>();

				bodyBall.Init(false, tailRig);

				var bodyBallRig = bodyballGO.GetComponent<Rigidbody2D>();
				var joint = bodyBallRig.GetComponent<HingeJoint2D>();
				joint.enabled = false;
				var lastbodyball = bodyBallList[bodyBallList.Count - 1];
				lastbodyball.SetNext(bodyBallRig);

				bodyballGO.transform.SetParent(circleList);
				bodyballGO.name = "BodyBall" + i.ToString();
			}

			bodyBallList.Add(bodyBall);
		}

		ChangeBodyBallVisiable();
	}


	public void ChangeBodyBallVisiable()
	{
		var bodyTypeList = GameManager.Instance.gamerData.BodyBallList;

		if (bodyTypeList.Count > maxCount)
		{
			
			maxCount = bodyTypeList.Count;
			// add virtual ball
			for (int i = bodyBallList.Count; i < maxCount; i++)
			{ 
				var bodyballprefab = GameManager.Instance.bodyBallPrefab;
				var bodyballGO = GameObject.Instantiate(bodyballprefab, transform.position + 2 * Vector3.one, Quaternion.identity);
				var bodyBall = bodyballGO.GetComponent<BodyBall>();

				bodyBall.Init(false, tailRig);

				var bodyBallRig = bodyballGO.GetComponent<Rigidbody2D>();
				var lastbodyball = bodyBallList[bodyBallList.Count - 1];
				lastbodyball.SetNext(bodyBallRig);

				bodyballGO.transform.SetParent(circleList);
				bodyballGO.name = "BodyBall" + i.ToString();

				bodyBallList.Add(bodyBall);
			}
		}

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

	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
			SceneManager.LoadScene(ConstData.SceneRunABall);
	}

}
