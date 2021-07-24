using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D coll;


    [Header("移动设置")]
    public float moveSpeed = 3f;

    [Header("移动状态")]
    public float xVelocity; // x轴的受力方向
    public float yVelocity; // y轴的受力方向

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //引用角色刚体
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		BallRun();
	}
    private void FixedUpdate()
    {
        
    }

    void BallRun()
    {
        xVelocity = Input.GetAxis("Horizontal");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        yVelocity = Input.GetAxis("Vertical");  //获取水平方向移动指令。-1f~1f 不按的时候自动归0 因此不会出现滑动
        rb.velocity = new Vector2(xVelocity * moveSpeed, yVelocity * moveSpeed);

    }

}
