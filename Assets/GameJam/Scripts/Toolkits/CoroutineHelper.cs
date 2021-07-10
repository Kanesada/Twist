using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
	private static CoroutineHelper _instance;
	public static CoroutineHelper Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("CoroutineManager").AddComponent<CoroutineHelper>() as CoroutineHelper;
			}
			return _instance;
		}
	}



	public IEnumerator DelayCall(float seconds, System.Action action)
	{
		if (action != null)
		{
			var einumerator = InnerDelayCall(seconds, action);
			StartCoroutine(einumerator);
			return einumerator;
		}
		return null;
	}
	private IEnumerator InnerDelayCall(float seconds, System.Action action)
	{
		yield return new WaitForSeconds(seconds);
		if (action != null)
			action.Invoke();
	}
}
