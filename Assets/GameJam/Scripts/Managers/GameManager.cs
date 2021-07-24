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

	public void DestroyMe()
	{
		instance = null;
	}
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

	[Header("��������")]
	public LevelData[] levelDatas;
	public GameObject playerPrefab;
	public GameObject headBallPrefab;
	public GameObject bodyBallPrefab;
	public GameObject[] BodyBallPrefabs;


	[Header("��Ϸʱ��")]
	public int totalTime = 100;
	[Header("nowTime")]
	public int nowTime = 0;

	
	public GameObject lostBallFVX;

	// �������
	public GamerData gamerData;

	[HideInInspector]
	public BodyBallControllerNew bodyBallController;
	[HideInInspector]
	public UIManager uiManager;
	[HideInInspector]
	public SpringBallController springBallController;

	public List<EndingData> endingList { get; private set; } = new List<EndingData>();
	public List<EndingData> levelEndingList { get; private set; }



	private bool timeFlag;




	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;

		gamerData = new GamerData();
		gamerData.Init();

		LoadEngindData();

		timeFlag = false;
	}

	private void GenerateLevel1()
	{
		timeFlag = true;
		nowTime = 0;
		totalTime = 100;
		uiManager.SetTimeText(nowTime, totalTime);

		for (int i = 0;i < levelDatas[0].initialBallCount; i++)
        {
			Vector3 pos = levelDatas[0].initialBallPosition[i];
			//print(pos);
			Instantiate(BodyBallPrefabs[i], pos, Quaternion.identity);
			
		}

		//bodyBallController.GenerateBody(4);
	}

	public void SetupSceneMainMenu()
	{
		timeFlag = false;
		gamerData.Init();
		totalTime = 100;
		nowTime = 0;
		bodyBallController = null;
		uiManager = null;
		levelEndingList = null;
		Time.timeScale = 1;
	}

	private void GenerateLevel2()
	{
		uiManager.SetTimeText(nowTime, totalTime);

		var artShowEndingGO = GameObject.Find("艺术生牌子");
		var peShowEndingGO = GameObject.Find("体育生牌子");

		if (gamerData.ChoosenEndings[0] == 1) // 选择了体育生的路线
		{
			artShowEndingGO?.SetActive(false);
			peShowEndingGO?.SetActive(true);
		}
		else if (gamerData.ChoosenEndings[0] == 2) // 选择了艺术生的路线
		{
			peShowEndingGO?.SetActive(false);
			artShowEndingGO?.SetActive(true);
		}


		for (int i = 0; i < levelDatas[1].initialBallCount; i++)
		{
			Vector3 pos = levelDatas[1].initialBallPosition[i];
			//print(pos);
			Instantiate(BodyBallPrefabs[i], pos, Quaternion.identity);

		}


	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == ConstData.SceneRunABall)
		{
			gamerData.LevelUp();
			GenerateLevel1();

			// �����г����п�ѡ���
			levelEndingList = GetEndingDatas();
			SetEndingLevel();
			// ����ѡ�񳡾����г��Ľ��
		}
		else if(scene.name == ConstData.SceneLevel02)
		{
			gamerData.LevelUp();
			GenerateLevel2();

			levelEndingList = GetEndingDatas();
			SetEndingLevel();
		}
		else if (scene.name == ConstData.SceneMainMenu)
		{
			SetupSceneMainMenu();
		}
		
	}

	// ��������һ���ؿ�ʱ���ã�����ؿ���ȥ�л��������Լ���ѡ��Ľ��ȥ�������·��
	public void OnLeaveLevel(string sceneName, int number)
	{
		gamerData.ChoosenEndings.Add(number);

		if (string.IsNullOrEmpty(sceneName)) // Game Good Over
		{
			uiManager.OnGameGoodOver();
			return;
		}
		SceneManager.LoadScene(sceneName);
	}


	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void AddSpringBodyBall(BallType type)
	{
		gamerData.AddBodyBall(type);
		springBallController.AddSpringBall(type);
	}

	public void RemoveSpringBodyBall(SpringBodyBall	spring)
	{
		int index = springBallController.GetBodyBallIndex(spring);
		if (index == 0) //意味着头撞到了陷阱
		{
			Debug.LogWarning("��Ϸ���� TODO");
			return;
		}
		gamerData.RemoveRestBodyBall(index);
		springBallController.RemoveSpringBall(spring);
	}


	public void AddBodyBall(BallType type)
	{
		gamerData.AddBodyBall(type);
		bodyBallController.ChangeBodyBallVisiable();
		AudioManager.PlayBallAddAudio();
	}

	public void RemoveBodyBall(BodyBall bodyBall)
	{
		int index = bodyBallController.GetBodyBallIndex(bodyBall);
		GameObject.Find("Player").GetComponent<PlayerMovement>().CameraShake();
		if (index == 0) //意味着头撞到了陷阱
		{
			// ��Ϸ������
			Debug.LogWarning("��Ϸ���� TODO");
			// ������Ч

			return;
		}
		Instantiate(lostBallFVX, bodyBall.transform.position, bodyBall.transform.rotation);  // add lost ball animation
		AudioManager.PlayBallLostAudio();

		gamerData.RemoveRestBodyBall(index);
		bodyBallController.ChangeBodyBallVisiable();
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

	private void SetEndingLevel()
	{
		var winzones = GameObject.FindGameObjectsWithTag("WinZone");


		for (int i = 0; i < winzones.Length; i++)
		{
			var winzone = winzones[i].GetComponent<WinZone>();

			winzone.SetEndingData(levelEndingList[i]);
		}
	}

	public List<string> GetChoosenEndingString()
	{
		List<string> endingDescribeList = new List<string>();
		var choosenList = gamerData.ChoosenEndings;
		foreach (var number in choosenList)
		{
			var index = number - 1;
			var endingData = endingList[index];
			endingDescribeList.Add(endingData.describe);
		}
		return endingDescribeList;
	}

	private void Update()
	{

		try
		{
			if (timeFlag == true)
			{

				totalTime = int.Parse(GameObject.Find("Canvas").GetComponent<UIManager>().text_time.text);
			}
		}
		catch (System.NullReferenceException ex)
		{
			Debug.Log("myLight was not set in the inspector");
		}

		
		

		if (Input.GetKeyDown(KeyCode.G))
		{
			var s = GetChoosenEndingString();
			foreach(var describe in s)
			{
				Debug.Log(describe);
			}
		}
	}

	

}
