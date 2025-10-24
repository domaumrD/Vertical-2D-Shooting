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
        if (collision.name == "Enemy Bullet 1(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator DoShield()
    {
        yield return new WaitForSeconds(durationTime);
        Destroy(this.gameObject);
    }

}
