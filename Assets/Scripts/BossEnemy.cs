using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossEnemy : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.down;

    private bool isCoolTime = false;

    private int patternIndex;
    public int currentPatternCount;
    public int[] maxPatternIndex;

    public Action<Vector3> action;
    public Action<int> addScoreAction;

    public enum State
    {
        Normal,
        Hit
    }

    public Transform shootingPoint;
    public Sprite[] stateSprites;
    public SpriteRenderer enemySprite;

    public int hp;
    public int score;

    public float moveSpeed;

    void Start()
    {
        this.isCoolTime = false;
        this.enemySprite.sprite = stateSprites[(int)State.Normal];
        this.moveDirection = Vector3.down;
        StartCoroutine(Attack());
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < 3)
        {
            moveDirection = Vector3.up;
            this.moveSpeed = 0.1f;
        }
        else if (this.transform.position.y > 3.3f)
        {
            moveDirection = Vector3.down;
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
            if (action != null)
            {
                addScoreAction(this.score);
            }

            if (action != null)
            {
                action(this.transform.position);
            }

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

    public void AddPatternIndex()
    {
        currentPatternCount = 0;

        if (patternIndex == 3)
        {
            patternIndex = 0;
        }
        else
        {
            patternIndex++;
        }
    }

    private void FireFoward()
    {
        currentPatternCount++;
        if(currentPatternCount >= maxPatternIndex[patternIndex])
        {
            AddPatternIndex();
            this.isCoolTime = true;
        }
        BulletManager.Instance.CreateSingleShot(shootingPoint);       
        Debug.Log("앞으로 4발 발사");
      
    }

    private void FireShot()
    {
        currentPatternCount++;
        if (currentPatternCount >= maxPatternIndex[patternIndex])
        {
            AddPatternIndex();
            this.isCoolTime = true;
        }
        
        BulletManager.Instance.CreateBossShotGun(shootingPoint);
 
        Debug.Log("플레이어 방향으로 샷건");
  
    }

    private void FireArc()
    {
        currentPatternCount++;
        if (currentPatternCount >= maxPatternIndex[patternIndex])
        {
            AddPatternIndex();
            this.isCoolTime = true;
        }
        BulletManager.Instance.CreateBossArcShot(shootingPoint, currentPatternCount, maxPatternIndex[patternIndex]);
        Debug.Log("부채모양으로 발사");

    }

    private void FireAround()
    {
        currentPatternCount++;
        if (currentPatternCount >= maxPatternIndex[patternIndex])
        {
            AddPatternIndex();
            this.isCoolTime = true;
        }
        BulletManager.Instance.CreateBossAroundShot(shootingPoint);
        Debug.Log("원 형태로 전체 공격");

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("공격");

        while (this.hp > 0)
        {            
            if (patternIndex == 0)
            {
                FireFoward();
                yield return new WaitForSeconds(1f);
            }
            else if (patternIndex == 1)
            {
                FireShot();
                yield return new WaitForSeconds(2f);
            }
            else if (patternIndex == 2)
            {
                FireArc();
                yield return new WaitForSeconds(0.15f);
            }
            else if (patternIndex == 3)
            {
                FireAround();
                yield return new WaitForSeconds(0.7f);
            }

            if (isCoolTime == true)
            {
                yield return new WaitForSeconds(3f);
                isCoolTime = false;
            }
        }

    }

}
