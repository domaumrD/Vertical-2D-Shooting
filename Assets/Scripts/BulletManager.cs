using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public GameObject playerBullet1Prefab;
    public GameObject enemyBullet0Prefab;
    public GameObject enemyBullet1Prefab;
    public GameObject enemyBulletParent;

    public void CreatePlayerBullet1(Transform transform)
    {
        GameObject go = Instantiate(playerBullet1Prefab);
        go.transform.position = transform.position;
    }

    public void CreateEnemyBullet0(Transform transform)
    {
        Vector3 moveDirection = PlayerData.Instance.GetPlayerPosition();
        GameObject go = Instantiate(enemyBullet0Prefab, enemyBulletParent.transform);
        go.transform.position = transform.position;
        EnemyBullet enemyBullet = go.GetComponent<EnemyBullet>();
        enemyBullet.Init();
    }

    public void CreateEnemyBullet1(Transform transform)
    {
        Vector3 moveDirection = PlayerData.Instance.GetPlayerPosition();
        GameObject go = Instantiate(enemyBullet1Prefab, enemyBulletParent.transform);
        go.transform.position = transform.position;
        EnemyBullet enemyBullet = go.GetComponent<EnemyBullet>();
        enemyBullet.Init(moveDirection);
    }

}
