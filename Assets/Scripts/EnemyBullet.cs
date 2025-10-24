using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 toMoveDirection;

    public float moveSpeed;
   
    void Update()
    {
        Move();
    }

    public void Move()
    {
        this.transform.Translate(toMoveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

        if (this.transform.position.y > 7)
        {
            Destroy(this.gameObject);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.name == "ShieldPrefab(Clone)")
        {
            Destroy(this.gameObject);
        }
    }


}
