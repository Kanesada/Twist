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
    float timeSpeed = 0.0f;
    //显示时间区域的文本
    public Text text_time;
    



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timeSpeed += Time.deltaTime;
        //hour = (int)timeSpeed / 3600;
        //minute = ((int)timeSpeed - hour * 3600) / 60;
        //second = (int)timeSpeed - hour * 3600 - minute * 60;
        //text_time.GetComponent<Text>().text= string.Format("{0:D3}:{1:D2}:{2:D2}", hour, minute, second);
        text_time.GetComponent<Text>().text = string.Format("{0}", timeSpeed.ToString("0"));

        if (timeSpeed.ToString("0").Equals("100")){
            //执行结束动画

        }











    }
}
