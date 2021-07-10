using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	#region Singleton
	private static GameManager instance;
	public static GameManager Instance => instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}
	#endregion

	public LevelData[] levelDatas;

	public GamerData gamerData;


	public GameObject playerPrefab;
	public GameObject[] BodyBallPrefabs;

	

	
	private void Start()
	{
		gamerData = new GamerData();
		print(levelDatas[0].levelNumber);
		print(levelDatas[0].initialBallPosition[0]);

	}


	public void GenerateLevel()
	{

	}




	
}
