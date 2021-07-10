using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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

	// 玩家数据
	public GamerData gamerData;

	public BodyBallControllerNew bodyBallController;

	private bool timeFlag;

	private List<EndingData> endingList = new List<EndingData>();


	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;

		gamerData = new GamerData();
		gamerData.Init();

		LoadEngindData();
		var endingCanBeSelectedList = GetEndingDatas();

		print(levelDatas[0].levelNumber);
		timeFlag = false;
	}

	private void GeneratePlayer(Vector3 position)
	{
		var player = GameObject.Instantiate(playerPrefab, position, Quaternion.identity);
	}

	private void GenerateLevel1()
	{
		timeFlag = true;
		
		
		for(int i = 0;i < levelDatas[0].initialBallCount; i++)
        {
			Vector3 pos = levelDatas[0].initialBallPosition[i];
			//print(pos);
			Instantiate(BodyBallPrefabs[i], pos, Quaternion.identity);
			
		}

		//bodyBallController.GenerateBody(4);
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

		// 播放音效
	}

	public void RemoveBodyBall(BodyBall bodyBall)
	{
		int index = bodyBallController.GetBodyBallIndex(bodyBall);
		if (index == 0) //意味着头撞到了陷阱
		{
			// 游戏结束？
			Debug.LogError("游戏结束 TODO");
			// 播放音效
			return;
		}

		gamerData.RemoveRestBodyBall(index);
		bodyBallController.ChangeBodyBallVisiable();
		// 播放音效
	}



	// 当进入下一个关卡时调用，输入关卡名，以及所选择的结局
	public void OnLevelUp(string sceneName,EndingData data)
	{
		gamerData.LevelUp();
		gamerData.ChoosenEndings.Add(data.number);
		SceneManager.LoadScene(sceneName);
	}


	private void LoadEngindData()
	{
		endingList.Clear();
		var data = Utils.ParseCSV("Ending", 1);
		foreach(var row in data)
		{
			int number = int.Parse(row[0]);
			int levelNeed = int.Parse(row[1]);
			string[] strs = row[2].Split(';');
			var list = new List<int>();
			for (int i = 0; i < strs.Length; i++)
			{
				int needNumber = int.Parse(strs[i]); 
				if (needNumber != 0)
					list.Add(needNumber);
			}
			var endingData = new EndingData
			{
				number = number,
				levelNeed = levelNeed,
				premiseNeed = list,
				describe = row[3]
			};
			endingList.Add(endingData);
		}
	}

	private List<EndingData> GetEndingDatas()
	{
		int level = gamerData.Level;
		var choosenEndings = gamerData.ChoosenEndings;

		var canBeSelectedEndingList = endingList.Where((data) =>
		{
			bool where = data.levelNeed == level;
			if (where == false)
				return false;
			for (int i = 0; i < data.premiseNeed.Count; i++)
			{
				where = where && choosenEndings.Contains(data.premiseNeed[i]);
			}
			return where;
		});

		return new List<EndingData>(canBeSelectedEndingList);
	}

	private void Update()
	{
		if (timeFlag == true)
		{

			totalTime = int.Parse(GameObject.Find("Canvas").GetComponent<UIManager>().text_time.text);
		}
		if (totalTime == 100)
		{
			//执行结束动画

		}


		//Test 代码
		//if (Input.GetKeyDown(KeyCode.R))
		//{
		//	bodyBallController.GenerateBody(4);

		//	GeneratePlayer(Vector3.one * 5);
		//}
	}

}
