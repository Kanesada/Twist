using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢��Ϸ��ҵ�����ʱ��Ϸ����
/// </summary>
public class GamerData 
{

	public void Init()
	{
		bodyBallList.Add(BallType.Life);
	}

	/// <summary>
	/// ��ҳԵ�����
	/// </summary>
	public List<BallType> bodyBallList = new List<BallType>();
	
	/// <summary>
	/// ����Ѿ�ѡ��Ľ��
	/// </summary>
	public List<int> choosenEndings = new List<int>();

	

}
