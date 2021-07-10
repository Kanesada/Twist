using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储游戏玩家的运行时游戏数据
/// </summary>
public class GamerData 
{


	/// <summary>
	/// 玩家吃到的球
	/// </summary>
	public List<BallType> bodyBallList = new List<BallType>();
	
	/// <summary>
	/// 玩家已经选择的结局
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
