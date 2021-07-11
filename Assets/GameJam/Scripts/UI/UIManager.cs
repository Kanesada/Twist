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
  
    //�Ѿ����ѵ�ʱ��
    
    //��ʾʱ��������ı�
    public Text text_time;
    public Text total_time;
    public int lose_seconds;
    public GameObject endPanel;

    float timeSpeed = 0.0f;
    private int totalnow;
	private bool hasLoseLifeThisFrame;

	// Start is called before the first frame update
	void Start()
    {
        total_time.GetComponent<Text>().text = "100";
        lose_seconds = 10;
        endPanel.SetActive(false);


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

        int now_int = int.Parse(text_time.GetComponent<Text>().text);
        int total_int = int.Parse(total_time.GetComponent<Text>().text);
        if (total_int - now_int <= 0)
        {
            //���panel ��Ϸ��ͣ �����µ�����
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

    public void OnClickBackBtn()
    {
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


}
