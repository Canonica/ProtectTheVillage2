using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
public abstract class CharacterEntity : MonoBehaviour, IDamageable
{

    [Header("Life")]
    public float maxLife;
    public float currentLife;

    [Header("Attack")]
    public float attackDamage;
    public float attackCooldown;

    [Header("Move")]
    public float MoveSpeed;
    public bool canMove = true;

    [Header("Invulnerability")]
    public float timerHitMax;
    public float timerHit;

    protected enum MoveState //enum for animations
    {
        Up,
        Down,
        Right,
        Left,
    }

    protected MoveState currentMoveState;

    private bool canTakeDamage = false;

    //******** Getter **************
    public float CurrentLife
    {
        get { return currentLife; }
    }

    //******** Getter / Setter **************
    public bool CanTakeDamage
    {
        get
        {
            return canTakeDamage;
        }

        set
        {
            canTakeDamage = value;
        }
    }

    public virtual void Start()
    {
        currentLife = maxLife;
    }

    public virtual void Update()
    {
        if (timerHit > 0)
        {
            timerHit -= Time.deltaTime;
        }
    }

    public virtual void TakeDamage(float damageTaken)
    {
        if (timerHit <= 0)
        {
            currentLife -= damageTaken;
            currentLife = Mathf.Max(0, CurrentLife);
            StopCoroutine(GetHitFeedBack());
            StartCoroutine(GetHitFeedBack());
            if (currentLife <= 0)
            {
                Death();
            }
            timerHit = timerHitMax;
        }
    }

    public virtual void HealDamage(float healTaken)
    {
        currentLife += healTaken;
        currentLife = Mathf.Min(maxLife, CurrentLife);
    }

    public abstract void Death();

    public abstract void Move();
    public abstract void Attack();

    //Return if the current entity is dead or not
    public bool IsDead()
    {
        if (currentLife <= 0)
        {
            return true;
        }
        return false;
    }

    public IEnumerator GetHitFeedBack()
    {
        if (GetComponentInChildren<SpriteRenderer>())
        {
            SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
            Color initialColor = Color.white;

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    sp.DOColor(Color.red, timerHitMax / 6);
                }
                else
                {
                    sp.DOColor(initialColor, timerHitMax / 6);
                }
                yield return new WaitForSeconds(timerHitMax / 6);
            }
            sp.DOKill(true);
            sp.color = initialColor;
        }
        yield return null;
    }
}