using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	[Header("≈‰÷√ ˝æ›")]
	public LevelData[] levelDatas;
	public GameObject playerPrefab;
	public GameObject[] BodyBallPrefabs;


	[HideInInspector]
	public GamerData gamerData;


	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}


	private void Start()
	{
		gamerData = new GamerData();
		print(levelDatas[0].levelNumber);
		print(levelDatas[0].initialBallPosition[0]);

	}


	private void GenerateLevel1()
	{
		print("∞¢Àπ∂Ÿ");
	}


	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == ConstData.ScenePlay)
		{
			GenerateLevel1();
		}
	}


	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}



}
