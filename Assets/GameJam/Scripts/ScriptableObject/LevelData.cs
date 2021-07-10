using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Scriptable/LevelData")]
public class LevelData : ScriptableObject
{
	/// <summary>
	/// �ؿ���
	/// </summary>
	public int levelNumber;

	/// <summary>
	/// �ؿ����ɵ�С����Ŀ
	/// </summary>
	public int initialBallCount;

	/// <summary>
	/// �ؿ����ɵ�С��λ��
	/// </summary>
	public Vector3[] initialBallPosition;

	/// <summary>
	/// ��һ��������ĳ���λ��
	/// </summary>
	public Vector3 firstBodyBallGenPosition;

	/// <summary>
	/// ����С�����ҳ���λ��
	/// </summary>
	public Vector3 playerGenPosition;

	
}
