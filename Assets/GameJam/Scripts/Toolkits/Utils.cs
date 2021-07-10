using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static void TrySetActive(GameObject go, bool isActive)
	{
		if (go != null && go.activeSelf != isActive)
		{
			go.SetActive(isActive);
		}
	}

	/// <summary>
	/// 解析CSV
	/// </summary>
	public static List<List<string>> ParseCSV(string csvResPath, int beginParseRow)
	{
		List<List<string>> dataList = new List<List<string>>();

		var ta = Resources.Load<TextAsset>(csvResPath);
		if (ta == null)
		{
			Debug.LogError("CSV文件不存在：" + csvResPath);
			return dataList;
		}
		string[] rowCollection = ta.text.Split('\n');
		for (int row = beginParseRow; row < rowCollection.Length; row++)
		{
			if (string.IsNullOrEmpty(rowCollection[row]))
				continue;
			rowCollection[row] = rowCollection[row].Replace("\r", "");
			string[] colCollection = rowCollection[row].Split(',');

			List<string> tempList = new List<string>();
			for (int col = 0; col < colCollection.Length; col++)
			{
				tempList.Add(colCollection[col]);
			}
			dataList.Add(tempList);
		}
		return dataList;
	}

}
