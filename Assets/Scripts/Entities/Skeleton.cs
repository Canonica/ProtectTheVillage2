using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : CharacterEntity
{
    GameObject village;
    Animator animator;
    string previousAnimation;
    Vector3 destination;

    [SerializeField]
    float distanceMaxToAttack;
    [SerializeField]
    GameObject boneProjectile;

    bool canAttack;

    public override void Start()
    {
        canAttack = true;
        base.Start();
        village = GameObject.FindGameObjectWithTag("Village");
        animator = GetComponentInChildren<Animator>();
        previousAnimation = "";
        destination = village.transform.position;
    }

    public override void Update()
    {
        base.Update();
        IA();
    }

    void IA()
    {
        NearestObjective();
        float distance = Vector3.Distance(transform.position, destination);
        if (distance > distanceMaxToAttack)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (canAttack)
        {
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        canAttack = false;
        Fire();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void Fire()
    {
        Vector3 direction = new Vector3(destination.x - transform.position.x, destination.y - transform.position.y, destination.z - transform.position.z);
        float boneRotation = Vector3.Angle(transform.forward, direction);
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(boneProjectile, transform.position, Quaternion.Euler(0, boneRotation, 0));

        bullet.GetComponent<BoneProjectile>().damage = attackDamage;
        bullet.GetComponent<BoneProjectile>().owner = this;
        bullet.GetComponent<BoneProjectile>().DoAnimation(this.transform.position, destination);
    }

    public override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        transform.position = Vector3.Lerp(transform.position, destination, MoveSpeed * Time.deltaTime);
        Vector3 direction = new Vector3(destination.x - transform.position.x, destination.y - transform.position.y, destination.z - transform.position.z);
        float angle = Vector3.Angle(transform.forward, direction);
        angle /= 90;
        angle = Mathf.Round(angle);
        switch ((int)angle)
        {
            case 0:
                SetAnimation("Up");
                break;
            case 1:
                float angle2 = Vector3.Angle(transform.right, direction);
                angle2 /= 90;
                angle2 = Mathf.Round(angle2);
                if (angle2 == 0)
                {
                    SetAnimation("Right");
                }
                else
                {
                    SetAnimation("Left");
                }
                break;
            case 2:
                SetAnimation("Down");
                break;
        }
    }

    void NearestObjective()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("NPC");
        List<GameObject> objects = new List<GameObject>();
        foreach (GameObject obj in temp)
        {
            objects.Add(obj);
        }
        //objects.Add(GameObject.FindGameObjectWithTag("Village"));
        objects.Add(GameObject.FindGameObjectWithTag("Player"));

        GameObject nearest = null;
        float distance = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            float tempFloat = Vector3.Distance(transform.position, objects[i].transform.position);
            if (i == 0 || distance > tempFloat)
            {
                distance = tempFloat;
                nearest = objects[i];
            }
        }

        destination = nearest.transform.position;
    }

    void SetAnimation(string animation)
    {
        if (previousAnimation != animation)
        {
            previousAnimation = animation;
            animator.SetTrigger(animation);
        }
    }
}
