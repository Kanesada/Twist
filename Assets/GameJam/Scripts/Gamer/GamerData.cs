using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢��Ϸ��ҵ�����ʱ��Ϸ����
/// </summary>
public class GamerData 
{


	/// <summary>
	/// ��ҳԵ�����
	/// </summary>
	public List<BallType> bodyBallList = new List<BallType>();
	
	/// <summary>
	/// ����Ѿ�ѡ��Ľ��
	/// </summary>
	public List<int> choosenEndings = new List<int>();

	public void Init()
	{
		bodyBallList.Add(BallType.Life);
		bodyBallList.Add(BallType.Life);
		bodyBallList.Add(BallType.Life);
	}

	public void AddBodyBall(BallType type)
	{
		bodyBallList.Add(type);
	}

	public void RemoveBodyBall(int index)
	{
		while(bodyBallList.Count > index)
		{
			bodyBallList.RemoveAt(bodyBallList.Count - 1);
		}
	}



}
