using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletIndex;
    public Vector3 toMoveDirection;
    public float moveSpeed;

    public Action<GameObject, int> ReturnEnemyBulletAction;
   
    void Update()
    {
        Move();
    }

    public void Move()
    {
        this.transform.Translate(toMoveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -7 || this.transform.position.y > 7)
        {
            UseCompleteEnemyBullet();
        }

        if (this.transform.position.x < -7 || this.transform.position.x > 7)
        {
            UseCompleteEnemyBullet();
        }
    }

    public void Init(Vector3 moveDirction)
    {
        this.toMoveDirection = (moveDirction - this.transform.position).normalized;
    }

    public void Init()
    {
        this.toMoveDirection = Vector3.down;
    }

    public void UseCompleteEnemyBullet()
    {
        ReturnEnemyBulletAction(this.gameObject, bulletIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.name == "ShieldPrefab(Clone)")
        {
            UseCompleteEnemyBullet();
        }
    }


}
