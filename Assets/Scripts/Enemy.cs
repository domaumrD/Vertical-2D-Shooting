using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Enemy : MonoBehaviour
{
    private float delta = 0f;

    public Action<Vector3> action;
    public Action<int> addScoreAction;

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

    public int hp;
    public int score;

    public float moveSpeed;
    public float delayTime;


    void Start()
    {
        this.enemySprite.sprite = stateSprites[(int)State.Normal];
        this.delta = 0f;
    }

   
    void Update()
    {
        Move();
        Attack();

    }

    private void Move()
    {
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void Attack()
    {
        this.delta += Time.deltaTime;

        if (this.delta > this.delayTime)
        {
            this.delta = 0f;

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
        if (collision.name == "Player Bullet 1(Clone)")
        {
            StartCoroutine(Hit());
            hp -= collision.GetComponent<PlayerBullet>().GetBulletDamage();
            Destroy(collision.gameObject);
        }

        if (this.hp <= 0)
        {
            addScoreAction(this.score);
            action(this.transform.position);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Hit()
    {
        this.enemySprite.sprite = stateSprites[(int)State.Hit];
        yield return new WaitForSeconds(0.1f);
        this.enemySprite.sprite = stateSprites[(int)State.Normal];
    }

    public int GetScore()
    {
        return this.score;
    }
}
