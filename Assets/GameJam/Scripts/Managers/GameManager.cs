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

	private void GenerateLevel2()
	{
		uiManager.SetTimeText(nowTime,totalTime);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == ConstData.SceneRunABall)
		{
			gamerData.LevelUp();
			GenerateLevel1();

			// �����г����п�ѡ���
			levelEndingList = GetEndingDatas();
			SetEndingLevel01();
			// ����ѡ�񳡾����г��Ľ��
		}
		else if(scene.name == ConstData.SceneLevel02)
		{
			gamerData.LevelUp();
			GenerateLevel2();

			levelEndingList = GetEndingDatas();
			SetEndingLevel02();
		}

		
	}

	// ��������һ���ؿ�ʱ���ã�����ؿ���ȥ�л��������Լ���ѡ��Ľ��ȥ�������·��
	public void OnLeaveLevel(string sceneName, EndingData data)
	{
		gamerData.ChoosenEndings.Add(data.number);
		SceneManager.LoadScene(sceneName);
	}


	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void AddBodyBall(BallType type)
	{
		gamerData.AddBodyBall(type);
		bodyBallController.ChangeBodyBallVisiable();
		AudioManager.PlayBallAddAudio();
		// ������Ч
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
		// ������Ч
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

	private void SetEndingLevel01()
	{
		var winzones1 = GameObject.FindGameObjectsWithTag("WinZone");


		for (int i = 0; i < winzones1.Length; i++)
		{
			var winzone = winzones1[i].GetComponent<WinZone>();

			winzone.SetEndingData(levelEndingList[i]);
		}
	}

	private void SetEndingLevel02()
	{
		var winzones2 = GameObject.FindGameObjectsWithTag("WinZone");

		for (int i = 0; i < winzones2.Length; i++)
		{
			var winzone = winzones2[i].GetComponent<WinZone>();

			winzone.SetEndingData(levelEndingList[i]);
		}
	}
}
