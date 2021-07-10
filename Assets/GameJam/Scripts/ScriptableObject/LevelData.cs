using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Scriptable/LevelData")]
public class LevelData : ScriptableObject
{
	/// <summary>
	/// 关卡号
	/// </summary>
	public int levelNumber;

	/// <summary>
	/// 关卡生成的小球数目
	/// </summary>
	public int initialBallCount;

	/// <summary>
	/// 关卡生成的小球位置
	/// </summary>
	public Vector3[] initialBallPosition;

	/// <summary>
	/// 第一个生命球的出生位置
	/// </summary>
	public Vector3 firstBodyBallGenPosition;

	/// <summary>
	/// 控制小球的玩家出生位置
	/// </summary>
	public Vector3 playerGenPosition;

	
}
