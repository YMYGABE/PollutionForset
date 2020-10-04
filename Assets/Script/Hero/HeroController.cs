using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    
    [Header("获取人物")]public Animator         p_animator;
    [Header("获取人物刚体")]public Rigidbody2D  p_rigidbody2D;
    
    [SerializeField]private float               p_speed; //序列化，下次打开unity时的赋值会被保存,且会在unity面板上看到
    [SerializeField] private float              p_JumpSpeed;

    private float                               inputX;
    private bool                                isGround = false;
    private HeroTools                           p_ground;
 
    // Start is called before the first frame update
    void Start()
    {
        p_animator = GetComponent<Animator>();
        p_rigidbody2D = GetComponent<Rigidbody2D>();
        //p_swordAttack1 = GetComponent<PolygonCollider2D>();
        p_ground = transform.Find("GroundSensor").GetComponent<HeroTools>();
    }

    // Update is called once per frame
    void Update()
    {
        //刚落地的时候
        //Debug.Log(p_ground.State());
        if (!isGround && p_ground.State())
        {
            isGround = true;
            p_animator.SetBool("Grounded", isGround);
        }
        //在空中的时候
        if (isGround && !p_ground.State())
        {
            isGround = false;
            p_animator.SetBool("Grounded", isGround);
        }
        //
        

        p_animator.SetFloat("AirSpeedy", p_rigidbody2D.velocity.y);
        inputX = Input.GetAxis("Horizontal"); //获取水平上的输入
        Move();
        Jump();
        SwapFacingDirection();
  
    }

    public void SwapFacingDirection()
    {
        if(inputX > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        if(inputX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            //GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void Move()
    {     
        p_rigidbody2D.velocity = new Vector2(inputX * p_speed, p_rigidbody2D.velocity.y);
        if (inputX != 0)
        {
            p_animator.SetBool("Run", true);
        }
        if(inputX == 0)
        {
            p_animator.SetBool("Run", false);
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            p_animator.SetTrigger("Jump");
            isGround = false;
            p_animator.SetBool("Grounded", isGround);
            p_rigidbody2D.velocity = new Vector2(p_rigidbody2D.velocity.x, p_JumpSpeed);
            p_ground.Disable(1f);
        }
    }
    
    
}
