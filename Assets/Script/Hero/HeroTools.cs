using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTools : MonoBehaviour
{
    private float p_DisableTime; 
    private int Count = 0;


    private void OnEnable()
    {
        Count = 0;
    }

    public bool State()
    {
        if(p_DisableTime > 0)
        {
            return false;
        }
        return Count > 0;
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        Count++;
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        Count--;
    }

    public void Disable(float duration)
    {
        p_DisableTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        p_DisableTime -= Time.deltaTime;
    }
}
