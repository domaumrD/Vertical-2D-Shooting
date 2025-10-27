using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : BaseEnemy
{
    private float delta = 0f;

    //public Action<Vector3> action;
    //public Action<int> addScoreAction;
    public Action<GameObject, int> ReturnEnemyAction;

    private Coroutine hitCoroutine;

    public enum BulletType
    {
        NORMAL,
        HIGHSPEED,
    }

    public enum State
    {
        Normal,
        Hit
    }

    public Transform shootingPoint;
    public Sprite[] stateSprites;
    public SpriteRenderer enemySprite;

    public BulletType bulletType;

    public int enemyIndex;
    //public int hp;
    //public int score;

    //public float moveSpeed;
    public float delayTime;

    protected override void Start()
    {
        enemySprite.sprite = stateSprites[(int)State.Normal];
        delta = 0f;
    }

    protected override void Update()
    {
       base.Update();
    }

    protected override void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            ReturnEnemyPool();
        }
    }
    protected override void Attack()
    {
        delta += Time.deltaTime;

        if (delta > delayTime)
        {
            delta = 0f;

            if (bulletType == BulletType.NORMAL)
            {
                BulletManager.Instance.CreateEnemyBullet1(shootingPoint);
            }
            else if (bulletType == BulletType.HIGHSPEED)
            {
                BulletManager.Instance.CreateEnemyBullet0(shootingPoint);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBullet>()!= null)
        {
            if(hitCoroutine != null)
            {
                StopCoroutine(hitCoroutine);
            }

            hitCoroutine = StartCoroutine(Hit());
            PlayerBullet playerBullet = collision.GetComponent<PlayerBullet>();

            hp -= playerBullet.GetBulletDamage();
            playerBullet.ReturnPlayerBulletPool();
        }

        if (hp <= 0)
        {           
            ReturnEnemyPool();
        }
    }

    IEnumerator Hit()
    {
        enemySprite.sprite = stateSprites[(int)State.Hit];
        yield return new WaitForSeconds(0.1f);
        enemySprite.sprite = stateSprites[(int)State.Normal];
    }

    public int GetScore()
    {
        return score;
    }

    public void ReturnEnemyPool()
    {
        addScoreAction(score);
        action(transform.position);
        ReturnEnemyAction(gameObject, enemyIndex);
    }
}
