
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : Enemy
{
    public Transform BlockX;
    public Transform BlockY;
    public Transform MoveTo;
    public Transform Player;
    public Animator E_animator;
    [Header("人物攻击时停止运动")]
    public float AttackFinishTime;
    public float startTime;
    [Header("设置攻击碰撞框存在时长")]
    public PolygonCollider2D swordAttack;
    public float SwordAttackStartTime;
    public float SwordAttackFinishTime;

    private bool startAttack = false;
    private float        waiteTime;
    private bool missingPlayer;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        missingPlayer = true;
        E_animator = GetComponent<Animator>();
        MoveTo.position = randomDirection();
        waiteTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (!startAttack) 
        {
            E_animator.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, MoveTo.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Player.position) < 3f)
            {
                missingPlayer = false;
                MoveTo.position = Player.position;

            }
            if (Vector2.Distance(transform.position, Player.position) > 10f 
                && missingPlayer.Equals(false))
            {
                MoveTo.position = randomDirection();
                missingPlayer = true;
            }
            else if (Vector2.Distance(transform.position, MoveTo.position) < 0.1f)
            {
                if (waiteTime <= 0)
                {

                    MoveTo.position = randomDirection();
                    waiteTime = startTime;
                }
                else
                {
                    E_animator.SetBool("Run", false);
                    waiteTime--;
                }
            }
            
        }
        if (health == 0)
        {
            GetComponent<GoblinController>().E_animator.SetTrigger("Death");
            GameObject.Destroy(gameObject);
        }
        Turn();
    }

    Vector2 randomDirection()
    {
        E_animator.SetBool("Run", true);
        Vector2 direction = new Vector2(Random.Range(BlockX.position.x, BlockY.position.x), transform.position.y);
        return direction;
    }

    public void Turn()
    {
        if(transform.position.x < MoveTo.position.x)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (transform.position.x > MoveTo.position.x)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
   
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") 
            && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            startAttack = true;
            E_animator.SetTrigger("Attack1");
            StartCoroutine(SwordAttackStart());
            E_animator.SetBool("Run", false);
            StartCoroutine(AttackStart());
        }
    }

    IEnumerator SwordAttackStart()
    {
        swordAttack.enabled = true;
        yield return new WaitForSeconds(SwordAttackStartTime);
        StartCoroutine(FinishAttack());
    }

    IEnumerator FinishAttack()
    {
        swordAttack.enabled = false;
        yield return new WaitForSeconds(SwordAttackFinishTime);
    }

    IEnumerator AttackStart()
    {

        yield return new WaitForSeconds(AttackFinishTime);
        startAttack = false;
    }

}
