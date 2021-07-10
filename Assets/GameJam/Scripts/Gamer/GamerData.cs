using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢��Ϸ��ҵ�����ʱ��Ϸ����
/// </summary>
public class GamerData 
{
	/// <summary>
	/// �ؿ�����
	/// </summary>
	public int Level { get; private set; }

	/// <summary>
	/// ��ҳԵ�����
	/// </summary>
	public List<BallType> BodyBallList { get; private set; } = new List<BallType>();
	
	/// <summary>
	/// ����Ѿ�ѡ��Ľ��
	/// </summary>
	public List<int> ChoosenEndings { get; private set; } = new List<int>();
		



	public void Init()
	{
		Level = 1;

		BodyBallList.Add(BallType.Life);
		BodyBallList.Add(BallType.Life);
		BodyBallList.Add(BallType.Life);
	}

	public void AddBodyBall(BallType type)
	{
		BodyBallList.Add(type);
	}

	public void RemoveRestBodyBall(int index)
	{
		while(BodyBallList.Count > index)
		{
			BodyBallList.RemoveAt(BodyBallList.Count - 1);
		}
	}

	public void OnLevelUp(int endingNumber)
	{
		Level += 1;
		ChoosenEndings.Add(endingNumber);
	}


}
