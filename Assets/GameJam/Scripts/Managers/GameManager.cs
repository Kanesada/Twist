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


	public GamerData gamerData;
	public BodyBallController bodyBallController;


	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}


	private void Start()
	{
		gamerData = new GamerData();
		bodyBallController = new BodyBallController();

		print(levelDatas[0].levelNumber);
		print(levelDatas[0].initialBallPosition[0]);

		TestRollABallScene();
	}


	private void TestRollABallScene()
	{

	}

	private void GenerateLevel1()
	{
		for(int i = 0;i< levelDatas[0].initialBallCount; i++)
        {
			Vector3 pos = levelDatas[0].initialBallPosition[i];
			
			Instantiate(BodyBallPrefabs[i], pos, Quaternion.identity);
			

		}
		
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



	public void AddBodyBall(BallType type)
	{
		print("AddBodyBall");
	}

}
