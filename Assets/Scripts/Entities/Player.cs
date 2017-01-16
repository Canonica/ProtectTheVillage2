using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterEntity {

    public GameObject lifeContainer;
    public bool isMoving;
    Animator animator;
    SpriteRenderer SprRend;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        SprRend = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
        currentMoveState = MoveState.Down;
    }


    // Update is called once per frame
    public override void Update()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 move = Vector3.zero;
        canMove = true;
        isMoving = false;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                move.z = 1;
                currentMoveState = MoveState.Up;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                move.x = -1;
                currentMoveState = MoveState.Right;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move.z = -1;
                currentMoveState = MoveState.Down;
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move.x = 1;
                currentMoveState = MoveState.Left;
                isMoving = true;
            }

            if (isMoving)
            {
                animator.speed = 1;
                switch (currentMoveState)
                {
                    case MoveState.Up:
                        animator.SetBool("goUp", true);
                        animator.SetBool("goLeft", false);
                        animator.SetBool("goRight", false);
                        SprRend.flipX = false;
                        break;
                    case MoveState.Down:
                        animator.SetBool("goUp", false);
                        animator.SetBool("goLeft", false);
                        animator.SetBool("goRight", false);
                        SprRend.flipX = false;
                        break;
                    case MoveState.Right:
                        animator.SetBool("goRight", false);
                        animator.SetBool("goLeft", true);
                        SprRend.flipX = true;
                        break;
                    case MoveState.Left:
                        animator.SetBool("goLeft", false);
                        animator.SetBool("goRight", true);
                        SprRend.flipX = false;
                        break;
                }
                GetComponent<Rigidbody>().MovePosition(transform.position + move.normalized * MoveSpeed * Time.deltaTime);
                //this.gameObject.transform.position += move.normalized * MoveSpeed * Time.deltaTime;
            }
            else
            {
                animator.speed = 0;
            }
        }
        else
        {
            animator.speed = 1;
        }
    }

    public override void Attack()
    {
        throw new NotImplementedException();
    }

    public override void Death()
    {
        throw new NotImplementedException();
    }


}
