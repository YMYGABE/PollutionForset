using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float health;

    public int CatchWall;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        CatchWall = 0;
        isPlayer = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool State()
    {
        return CatchWall > 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CatchWall++;
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }

    public void Takedamage(float damage)
    {
        health -= damage;
        GetComponent<GoblinController>().E_animator.SetTrigger("TakeHit");
        
    }
}
