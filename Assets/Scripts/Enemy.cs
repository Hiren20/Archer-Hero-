using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RootMotion;
using RootMotion.Dynamics;

public class Enemy : MonoBehaviour
{
    public Animator anim;

    public GameObject player;

    public float movementSpeed = 10f;

    bool canMove = false;

    public PuppetMaster puppet;



    public void ActivateEnemy()
    {
        anim.SetTrigger("Walk");

        canMove = true;
    }

    private void Update()
    {
        //transform.LookAt(player.transform.position);
        Move();

        
    }

    private void Move()
    {
        //Move
        if (canMove)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
        
    }

    public void Dead()
    {
        anim.SetTrigger("Idle");
        canMove = false;
        puppet.state = PuppetMaster.State.Dead;
    }


}
