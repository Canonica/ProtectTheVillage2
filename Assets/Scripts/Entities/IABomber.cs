using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABomber : CharacterEntity
{
    bool canAttack;
    GameObject target;
	[SerializeField]
	GameObject bomb;

    public override void Start()
    {
        base.Start();
        canAttack = true;
    }

    public override void Update()
    {
        base.Update();
        IA();
    }

    void IA()
    {
        if (canAttack)
        {
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        Attack();
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void getNearest()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float distance = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            float temp = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (i == 0 || temp < distance)
            {
                distance = temp;
                nearest = enemies[i];
            }
        }
        target = nearest;
    }

    public override void Attack()
	{
		getNearest();
		GameObject bullet = Instantiate (bomb, transform.position, Quaternion.identity) as GameObject;
		bullet.GetComponent<BombProjectile> ().setTarget (target, attackDamage);
    }

    public override void Death()
    {
        Destroy(this.gameObject);
    }

    public override void Move()
    {
    }
}