using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    public AIPath aIPath;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void EnemyDeath()
    {
        anim.SetTrigger("Dead"); 
    }
    void Update()
    {
        if (GameManager.instance.isDialogueDisplayed)
        {
            StopAI();
            return;
        }
        if (GetComponent<Renderer>().isVisible)
        {
            aIPath.canMove = true;
            aIPath.maxSpeed = 6f;
            aIPath.isStopped = false;
            if (aIPath.desiredVelocity.x <= -0.01f)
                transform.localScale = new Vector3(-0.15f, 0.135f, 1f);
            else if (aIPath.desiredVelocity.x >= 0.01f)
                transform.localScale = new Vector3(0.15f, 0.135f, 1f);
        }
        else
        {
            StopAI();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            StopAI();
            anim.SetTrigger("NearPlayer");
            GameManager.PlayerDeathEvent?.Invoke();
        }
    }
    void StopAI()
    {
        aIPath.maxSpeed = 0;
        aIPath.isStopped = true;
    }
}
