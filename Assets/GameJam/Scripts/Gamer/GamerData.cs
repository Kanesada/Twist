using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储游戏玩家的运行时游戏数据
/// </summary>
public class GamerData 
{

	public void Init()
	{
		bodyBallList.Add(BallType.Life);
	}

	/// <summary>
	/// 玩家吃到的球
	/// </summary>
	public List<BallType> bodyBallList = new List<BallType>();
	
	/// <summary>
	/// 玩家已经选择的结局
	/// </summary>
	public List<int> choosenEndings = new List<int>();

	

}
