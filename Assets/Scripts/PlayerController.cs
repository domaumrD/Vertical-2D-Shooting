using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float ypos;
    private float xpos;
    private float ybound;
    private float xbound;
    private float delta;
    private float curTime;

    public bool isgameOver = false;

    public bool isHit = false;
    public bool isShooting = false;

    public float moveSpeed;
    public float shootingDelyTime;
    public float maxYPos;

    public Transform shootingPoint;
    public Transform shieldPoint;

    public FixedJoystick fixedJoystick;

    public Animator Animator;

    public Action hitAction;
    public Action<int> addScoreAction;
    public Action useBoomAction;
    public Action getBoomAction;
    public Action shieldAction;

    public int boomCount; // 폭탄 실제 갯수

    void Start()
    {
        isgameOver = false;
        isHit = false;
        isShooting = false; 
        boomCount = 3;
        maxYPos = 4.5f;
    }

    void Update()
    {
        if (isgameOver == false && isHit == false)
        {
            //Move();
            //Attack();
            StickMove();
            ShootBullet();
        }
    }

    private void StickMove()
    {

        ypos = fixedJoystick.Vertical;
        xpos = fixedJoystick.Horizontal;

        Vector2 dir = new Vector2(xpos, ypos).normalized;
        this.transform.Translate(dir * moveSpeed * Time.deltaTime);
        ybound = transform.position.y;
        xbound = transform.position.x;
        ybound = Mathf.Clamp(ybound, -4.5f, maxYPos);
        xbound = Mathf.Clamp(xbound, -2.3f, 2.3f);
        this.transform.position = new Vector2(xbound, ybound);

        if (xpos > 0.5f)
        {
            Animator.SetInteger("State", 1);
        }
        else if (xpos < -0.5f)
        {
            Animator.SetInteger("State", 2);
        }
        else
        {
            Animator.SetInteger("State", 0);
        }

    }

    private void Move()
    {
        ypos = Input.GetAxisRaw("Vertical");
        xpos = Input.GetAxisRaw("Horizontal");

        Vector2 dir = new Vector2(xpos, ypos).normalized;
        this.transform.Translate(dir * moveSpeed * Time.deltaTime);

        ybound = transform.position.y;
        xbound = transform.position.x;

        ybound = Mathf.Clamp(ybound, -4.5f, 4.5f);
        xbound = Mathf.Clamp(xbound, -2.3f, 2.3f);

        this.transform.position = new Vector2(xbound, ybound);

        if (xpos > 0.5f)
        {
            Animator.SetInteger("State", 1);
        }
        else if (xpos < -0.5f)
        {
            Animator.SetInteger("State", 2);
        }
        else
        {
            Animator.SetInteger("State", 0);
        }
    }

    private void Attack()
    {
        if (delta > shootingDelyTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                BulletManager.Instance.CreatePlayerBullet1(shootingPoint);
                delta = 0f;
            }
        }

        delta += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.B))
        {           
            useBoomAction();
            SubtractBoomCount();
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    public void StartShooting()
    {
        isShooting = true;
        delta = shootingDelyTime; // 즉시 한 발 쏘고 싶으면 유지
        Debug.Log("플레이어 슈팅 시작");
    }

    public void StopShooting()
    {
        isShooting = false;
        Debug.Log("플레이어 슈팅 종료");
    }

    public void ShootBullet()
    {
        if (isShooting)
        {
            curTime += Time.deltaTime;

            if (curTime > shootingDelyTime)
            {
                BulletManager.Instance.CreatePlayerBullet1(shootingPoint);
                curTime = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyBullet>() != null)
        {
            EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
            enemyBullet.UseCompleteEnemyBullet();
            isHit = true;
            hitAction();
        }
        else if (collision.name == "CoinPrefab(Clone)")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            addScoreAction(item.GetItemScore());
            Debug.Log("Get Coin");
            item.UseCompleteItem();
        }
        else if (collision.name == "BoomPrefab(Clone)")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            addScoreAction(item.GetItemScore());
            Debug.Log("Get Boom");
            AddBoomCount();
            getBoomAction();
            item.UseCompleteItem();
        }
        else if (collision.name == "PowerPrefab(Clone)")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            addScoreAction(item.GetItemScore());
            Debug.Log("Get Power");
            item.UseCompleteItem();
        }
        else if(collision.name == "ShieldItemPrefab(Clone)")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            addScoreAction(item.GetItemScore());
            Debug.Log("Get Power");
            shieldAction();
            item.UseCompleteItem();
        }

            Debug.Log(collision.name);
    }

    public void StopAction()
    {
        isgameOver = true;
        this.gameObject.SetActive(false);
    }

    public int GetPlayerBoomCount()
    {
        return boomCount;
    }

    public void AddBoomCount()
    {
        this.boomCount++;

        if (this.boomCount > 3) 
        {
            this.boomCount = 3;
        }
    }

    public void SubtractBoomCount()
    {
        this.boomCount--;

        if (this.boomCount < 0)
        {
            this.boomCount = 0;
        }
    }

    public void UseBoom()
    {
        useBoomAction();
        SubtractBoomCount();
    }

}
