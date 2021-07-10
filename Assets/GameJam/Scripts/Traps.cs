using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "HeadBall")
        {
            //GameManager.GameOver();    当头节点触碰陷阱时 触发角色死亡
            Debug.Log("Header Ball Traped!!");
        }

        if(collision.gameObject.tag == "BodyBall")
        {
            // 触发丢球函数 （音效 动画等）
            Debug.Log("Body Ball Ttaped!!!");
        }
    }
}
