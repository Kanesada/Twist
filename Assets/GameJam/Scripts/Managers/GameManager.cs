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

	[Header("配置数据")]
	public LevelData[] levelDatas;
	public GameObject playerPrefab;
	public GameObject headBallPrefab;
	public GameObject bodyBallPrefab;
	public GameObject[] BodyBallPrefabs;


	[Header("游戏时间")]
	public int totalTime;

	public GamerData gamerData;
	public BodyBallController bodyBallController;
	private bool timeFlag;
	

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
		timeFlag = false;

		TestRollABallScene();
	}


	private void TestRollABallScene()
	{

	}

	private void GeneratePlayer(Vector3 position)
	{
		var player = GameObject.Instantiate(playerPrefab, position, Quaternion.identity);
	}

	private void GenerateLevel1()
	{
		timeFlag = true;
		
		
		for(int i = 0;i< levelDatas[0].initialBallCount; i++)
        {
			Vector3 pos = levelDatas[0].initialBallPosition[i];
			//print(pos);
			Instantiate(BodyBallPrefabs[i], pos, Quaternion.identity);
			
		}
		
	}


	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == ConstData.SceneRunABall)
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
		gamerData.AddBodyBall(type);
		bodyBallController.ChangeBodyBallVisiable();
	}

	public void RemoveBodyBall(BodyBall bodyBall)
	{
		int index = bodyBallController.GetBodyBallIndex(bodyBall);
		gamerData.RemoveBodyBall(index);
		bodyBallController.ChangeBodyBallVisiable();
	}

	private void Update()
    {
		if(timeFlag == true)
        {

			totalTime = int.Parse(GameObject.Find("Canvas").GetComponent<UIManager>().text_time.text);
		}
		if (totalTime == 100)
		{
			//执行结束动画

		}


		//Test 代码
		if (Input.GetKeyDown(KeyCode.R))
		{
			bodyBallController.GenerateBody(4);

			GeneratePlayer(Vector3.one*5);
		}
	}

}
