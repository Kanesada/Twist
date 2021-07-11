using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    //private int hour;
    //private int minute;
    //private int second;
  
    //已经花费的时间
    
    //显示时间区域的文本
    public Text text_time;
    public Text total_time;
    public int lose_seconds;
    public GameObject endPanel;

    float timeSpeed;
    private int totalnow;
	private bool hasLoseLifeThisFrame;
    public int now_int;
    public int total_int;

    private void Awake()
    {
        GameManager.Instance.uiManager = this;
        timeSpeed = (float)GameManager.Instance.nowTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        total_time.GetComponent<Text>().text = GameManager.Instance.totalTime.ToString();
        lose_seconds = 10;
        endPanel.SetActive(false);
         now_int = int.Parse(text_time.GetComponent<Text>().text);
         total_int = int.Parse(total_time.GetComponent<Text>().text);

    }

    // Update is called once per frame
    void Update()
    {
		hasLoseLifeThisFrame = false;

		timeSpeed += Time.deltaTime;
        //hour = (int)timeSpeed / 3600;
        //minute = ((int)timeSpeed - hour * 3600) / 60;
        //second = (int)timeSpeed - hour * 3600 - minute * 60;
        //text_time.GetComponent<Text>().text= string.Format("{0:D3}:{1:D2}:{2:D2}", hour, minute, second);
        text_time.GetComponent<Text>().text = string.Format("{0}", timeSpeed.ToString("0"));

        
        now_int = int.Parse(text_time.GetComponent<Text>().text);
        total_int = int.Parse(total_time.GetComponent<Text>().text);
        if (total_int - now_int <= 0 || now_int == 100)
        {
            endPanel.SetActive(true);
            Time.timeScale = 0f;
            //结局panel 游戏暂停 播放新的音乐
        }


        GameManager.Instance.totalTime = total_int;
        GameManager.Instance.nowTime = now_int;

    }

    public void LoseLife()
    {
		if (hasLoseLifeThisFrame == true)
			return;

		hasLoseLifeThisFrame = true;

		totalnow = int.Parse(total_time.GetComponent<Text>().text);
        totalnow -= lose_seconds;
        total_time.GetComponent<Text>().text = totalnow.ToString();

    }

    public void OnClickBackBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(ConstData.SceneMainMenu);
    }


    public void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void SetTimeText(int nowTime,int totalTime)
    {
        text_time.text = nowTime.ToString();
        total_time.text = totalTime.ToString();
    }
}
