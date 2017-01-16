using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : CharacterEntity {

   
    Animator animator;
    SpriteRenderer SprRend;
    [Header("Characteristics")]
    public int strength;
    public int agility;

    [Header("Minage")]
    public float minageSpeed;

    [Header("Gold")]
    public int maxGold;
    public int currentNbOfGold;

    [Header("Other")]
    public GameObject lifeContainer;
    public bool isMoving;

    [Header("UIText")]
    public Text goldText;
    public Text strengthText;
    public Text agilityText;
    public Text bomberText;
    public Text lifeText;

    public NPC currentNPC;
    public bool isNearNPC;

    public bool isNearMine;
    bool isMining;

    public GameObject bomberPrefab;
    public int currentNbOfBomber;

    public Image actionBar;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        SprRend = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
        currentMoveState = MoveState.Down;
        UpdateUI();
    }

    // Update is called once per frame
    public override void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isNearMine && !isMining)
            {
                isMining = true;
                StartCoroutine("Minage");
            }
            if (isNearNPC && currentNPC != null)
            {
                currentNPC.AddStat();
                UpdateUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnBomber();
        }
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
                GetComponent<Rigidbody>().MovePosition(transform.position + move.normalized * (MoveSpeed+agility) * Time.deltaTime);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Minage()
    {
        canMove = false;
        yield return new WaitForSeconds(minageSpeed - ((strength + agility)*0.05f));
        
        if (isNearMine)
        {
            currentNbOfGold += 1;
        }
        UpdateGold();
        canMove = true;
        isMining = false;
    }

    void UpdateUI()
    {
        UpdateGold();
        UpdateStrength();
        UpdateAgility();
        UpdateLife();
        UpdateBomber();
    }

    void UpdateGold()
    {
        goldText.text = currentNbOfGold.ToString();
    }

    void UpdateStrength()
    {
        strengthText.text = "Strength : "+strength.ToString();
    }

    void UpdateAgility()
    {
        agilityText.text = "Agility : " + agility.ToString();
    }

    void UpdateLife()
    {
        lifeText.text = currentLife + "/" + maxLife;
        lifeText.transform.parent.GetComponent<Image>().fillAmount = currentLife / maxLife;
    }

    void UpdateBomber()
    {
        bomberText.text = "Bombers : "+ currentNbOfBomber;
    }

    void SpawnBomber()
    {
        if(currentNbOfBomber > 0)
        {
            Instantiate(bomberPrefab, transform.position, transform.rotation);
            currentNbOfBomber--;
        }
        
    }
}
