using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储游戏玩家的运行时游戏数据
/// </summary>
public class GamerData 
{
	/// <summary>
	/// 关卡数量
	/// </summary>
	public int Level { get; private set; }

	/// <summary>
	/// 玩家吃到的球
	/// </summary>
	public List<BallType> BodyBallList { get; private set; } = new List<BallType>();
	
	/// <summary>
	/// 玩家已经选择的结局
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
