using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    float timeSpeed = 0.0f;
    private int totalnow;
	private bool hasLoseLifeThisFrame;

	// Start is called before the first frame update
	void Start()
    {
        total_time.GetComponent<Text>().text = "100";
        lose_seconds = 10;


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


        if (total_time.GetComponent<Text>().text.Equals(text_time.GetComponent<Text>().text))
        {
            Debug.Log("游戏结束");
        }



       
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





}
