using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoPath : MonoBehaviour
{
    [Header("获取人物的四角碰撞框")]
    private Enemy LeftUp;
    private Enemy LeftDown;
    private Enemy RightUp;
    private Enemy RightDown;
    [Header("设置怪物跳跃高度")]
    public float JumpSpeed;
    [Header("获取怪物物理")]
    public Rigidbody2D Enemyrigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        LeftUp = transform.Find("leftUp").GetComponent<Enemy>();
        LeftDown = transform.Find("leftDown").GetComponent<Enemy>();
        RightUp = transform.Find("rightUp").GetComponent<Enemy>();
        RightDown = transform.Find("rightDown").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        CheakCanJump();
    }

    public void CheakCanJump()
    {
        Debug.Log(LeftDown.State());
        if (!GetComponent<Enemy>().isPlayer) 
        { 
            if (LeftDown.State() || RightDown.State())
        //( && !(LeftUp.State() || RightUp.State()))
            {
                LeftDown.CatchWall = 0;
                RightDown.CatchWall = 0;
                Enemyrigidbody2d.velocity = new Vector2(Enemyrigidbody2d.velocity.x, JumpSpeed);
            // Debug.Log(LeftDown.State());
            }
        }

    }
}