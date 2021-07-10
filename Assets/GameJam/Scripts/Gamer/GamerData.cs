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
	public List<BodyBallType> bodyBallList = new List<BodyBallType>();
	
	/// <summary>
	/// 玩家已经选择的结局
	/// </summary>
	public List<int> choosenEndings = new List<int>();

	

}
