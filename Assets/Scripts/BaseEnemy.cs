using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int hp;
    public int score;
    public float moveSpeed;

    public Action<Vector3> action;
    public Action<int> addScoreAction;
    public Action<Vector3> spawnEffectAction;

    protected bool isDead = false;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
            Attack();
        }
    }
    
    protected abstract void Move();
    protected abstract void Attack();

    public int GetScoreValue()
    {
        return score;
    }

    public virtual void ProcessScore()
    {
        addScoreAction?.Invoke(score);
        spawnEffectAction?.Invoke(transform.position);
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead)
            return;

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        ProcessScore();
    }
}

