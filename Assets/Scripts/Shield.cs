using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float durationTime;

    private void Start()
    {
        StartCoroutine(DoShield());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBullet>() != null)
        {
            EnemyBullet enemyBullet = collision.GetComponent<EnemyBullet>();
            enemyBullet.UseCompleteEnemyBullet();      
        }
    }

    IEnumerator DoShield()
    {
        yield return new WaitForSeconds(durationTime);
        Destroy(this.gameObject);
    }

}
