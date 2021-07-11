using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [Header("头节点")]
    public GameObject headerBall; // 获取头节点
    [Header("胜利区域外力向量")]
    public Vector2 direction; 
    Rigidbody2D headerBallRb;

	public EndingData endingData { get; private set; }

    private void Start()
    {
        headerBallRb = headerBall.GetComponent<Rigidbody2D>();  // 获取头节点刚体
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HeadBall")
        {
            //GameManager.PlayerWon();
            headerBallRb.AddForce(direction,ForceMode2D.Impulse);  //施加外力拖向远方
            Debug.Log(" Force complete");
            AudioManager.PlayWinAudio(); // 播放胜利音效

			CoroutineHelper.Instance.DelayCall(2f, () =>
			{
				if (GameManager.Instance.gamerData.Level == 1)
				{ 
					GameManager.Instance.OnLeaveLevel(ConstData.SceneLevel02, endingData);
				}
			});
        }
    }

	public void SetEndingData(EndingData endingData)
	{
		this.endingData = endingData;
	}


}
