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

    private bool isgameOver = false;
    public bool isHit = false;

    public float moveSpeed;
    public float shootingDelyTime;

    public Transform shootingPoint;
    public Transform shieldPoint;

    public Animator Animator;

    public Action hitAction;
    public Action<int> addScoreAction;
    public Action useBoomAction;
    public Action getBoomAction;
    public Action shieldAction;

    public int boomCount; // ÆøÅº ½ÇÁ¦ °¹¼ö

    void Start()
    {
        isgameOver = false;
        isHit = false;
        boomCount = 3;
    }

    void Update()
    {
        if (isgameOver == false && isHit == false)
        {
            Move();
            Attack();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Enemy Bullet 0(Clone)")
        {
            Destroy(collision.gameObject);
            isHit = true;
            hitAction();
        }
        else if (collision.name == "Enemy Bullet 1(Clone)")
        {
            Destroy(collision.gameObject);
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

}
