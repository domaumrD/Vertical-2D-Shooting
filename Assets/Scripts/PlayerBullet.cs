using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int bulletIndex;
    public float bulletSpeed;
    public int damage;
    public Action<GameObject, int> ReturnPlayerBulletAction;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        if (this.transform.position.y > 7)
        {
            ReturnPlayerBulletPool();
        }
    }

    public int GetBulletDamage()
    { 
        return damage; 
    }

    public void ReturnPlayerBulletPool()
    {
        ReturnPlayerBulletAction(this.gameObject, this.bulletIndex);
    }

}
