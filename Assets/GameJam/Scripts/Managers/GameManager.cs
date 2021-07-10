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
	public GameObject bodyBallPrefab;

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
		gamerData.Init();

		bodyBallController = new BodyBallController();

		print(levelDatas[0].levelNumber);
		print(levelDatas[0].initialBallPosition[0]);

		TestRollABallScene();
	}


	private void TestRollABallScene()
	{
		bodyBallController.GenerateBody(3);
	}

	private void GenerateLevel1()
	{
		print(levelDatas[0].initialBallCount);
		for(int i = 0;i< levelDatas[0].initialBallCount; i++)
        {
			Vector3 pos = levelDatas[0].initialBallPosition[i];
			print(pos);


			var go = Instantiate(BodyBallPrefabs[0], pos, Quaternion.identity);
			go.transform.position = pos;

			print(go.transform.position);

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
