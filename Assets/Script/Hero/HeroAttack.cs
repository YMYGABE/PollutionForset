using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [Header("获得攻击第一段的碰撞框")] public PolygonCollider2D p_swordAttack1;
    [SerializeField] private float startFirstAttackTime;
    [SerializeField] private float finishFirstTime;
    [Header("获得攻击第二段的碰撞框")] public PolygonCollider2D p_swordAttack2;
    [SerializeField] private float startSecondAttackTime;
    [SerializeField] private float finishSecondTime;
    [Header("获得攻击第三段的碰撞框")] public PolygonCollider2D p_swordAttack3;
    [SerializeField] private float startThirdAttackTime;
    [SerializeField] private float finishThirdTime;
    [Header("攻击间隔")]
    [SerializeField]private float WaitForNextAttack;
    [Header("")]
    public Animator p_animator;
    
    private float p_AttrackTime = 0.0f;
    private float p_currentAttrack = 0;

    // Start is called before the first frame update
    void Start()
    {
        p_animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        p_AttrackTime += Time.deltaTime;
        Attrack();
        
    }
    public void Attrack()
    {
        if (Input.GetMouseButtonDown(0) && p_AttrackTime > 0.25f)
        {
            p_currentAttrack++;
           
            StartCoroutine(WaitAttack());
            
            if (p_currentAttrack > 3)
            {
                p_currentAttrack = 1;
            }
            if(p_AttrackTime > 1f)
            {
                p_currentAttrack = 1;
            }
            Debug.Log(p_currentAttrack);
            p_animator.SetTrigger("Attrack" + p_currentAttrack);
            StartCoroutine(StartAttack());
            //Debug.Log(p_currentAttrack);
            p_AttrackTime = 0;
        }
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(WaitForNextAttack);
        
    }
    IEnumerator StartAttack()
    {
        
        
        if (p_currentAttrack == 1)
        {
            yield return new WaitForSeconds(startFirstAttackTime);
            p_swordAttack1.enabled = true;
          
        }
        if (p_currentAttrack == 2)
        {
            yield return new WaitForSeconds(startSecondAttackTime);
            p_swordAttack2.enabled = true;
        
        }
        if (p_currentAttrack == 3)
        {
            yield return new WaitForSeconds(startThirdAttackTime);
            p_swordAttack3.enabled = true;
    
        }     
        StartCoroutine(finishAttack());
    } 
    IEnumerator finishAttack()
    {
        if (p_currentAttrack == 1)
        { 
            yield return new WaitForSeconds(finishFirstTime);
            p_swordAttack1.enabled = false;
        }

        if (p_currentAttrack == 2)
        {
            yield return new WaitForSeconds(finishSecondTime);
            p_swordAttack2.enabled = false;
        }

        if (p_currentAttrack == 3)
        {
            yield return new WaitForSeconds(finishThirdTime);
            p_swordAttack3.enabled = false;
        }
       

    }

    
}
