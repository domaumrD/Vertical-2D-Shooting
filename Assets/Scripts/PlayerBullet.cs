using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        if (this.transform.position.y > 7)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetBulletDamage()
    { 
        return damage; 
    }

}
