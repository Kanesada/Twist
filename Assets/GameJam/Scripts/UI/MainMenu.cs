using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnChoose()
	{
		SceneManager.LoadScene(ConstData.SceneChoose);
		print("OnChoose");
	}


	public void OnPlay()
	{
		SceneManager.LoadScene(ConstData.SceneRunABall);
		print("OnSceneRunABall");
	}


	public void OnOption()
	{
		SceneManager.LoadScene(ConstData.SceneOption);
		print("OnOption");
	}


	public void OnManuel()
	{
		SceneManager.LoadScene(ConstData.SceneManuel);
		print("OnManuel");
	}


	public void OnExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

}
