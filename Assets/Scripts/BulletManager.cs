using System.Collections;
using System.Collections.Generic;
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

        EnemyBulletPrefabInit();
        BossBulletPrefabInit();
        PlayerBulletPrefabInit();
    }

    public bool isAroundFlag = true;
    public GameObject playerBulletParent;

    public List<GameObject> playerBulletPrefab;
    public List<Queue<GameObject>> playerBulletPool = new List<Queue<GameObject>>();

    public GameObject enemyBulletParent;

    public List<GameObject> enemyBulletPrefab = new List<GameObject>();
    public List<Queue<GameObject>> enemyBulletPool = new List<Queue<GameObject>>();

    public List<GameObject> bossBulletPrefab = new List<GameObject>();
    public List<Queue<GameObject>> bossBulletPool = new List<Queue<GameObject>>();


    public void EnemyBulletPrefabInit()
    {
        for (int i = 0; i < enemyBulletPrefab.Count; i++)
        {
            Queue<GameObject> que = new Queue<GameObject>();

            for (int j = 0; j < 30; j++)
            {
                GameObject go = Instantiate(enemyBulletPrefab[i], enemyBulletParent.transform);
                go.SetActive(false);
                que.Enqueue(go);
            }

            enemyBulletPool.Add(que);
        }
    }

    public void BossBulletPrefabInit()
    {
        for (int i = 0; i < bossBulletPrefab.Count; i++)
        {
            Queue<GameObject> que = new Queue<GameObject>();

            int temp = i;

            if (temp == 1)
            {
                temp = 150;
            }
            else if (temp == 3)
            {
                temp = 250;
            }
            else
            {
                temp = 0;
            }

            for (int j = 0; j < 50 + temp; j++)
            {
                GameObject go = Instantiate(bossBulletPrefab[i], enemyBulletParent.transform);
                go.SetActive(false);
                que.Enqueue(go);
            }

            bossBulletPool.Add(que);
        }
    }

    public void PlayerBulletPrefabInit()
    {
        for (int i = 0; i < playerBulletPrefab.Count; i++)
        {
            Queue<GameObject> que = new Queue<GameObject>();

            for (int j = 0; j < 100; j++)
            {
                GameObject go = Instantiate(playerBulletPrefab[i], playerBulletParent.transform);
                go.SetActive(false);
                que.Enqueue(go);
            }

            playerBulletPool.Add(que);
        }
    }

    public void CreatePlayerBullet1(Transform transform)
    {
        GameObject go = playerBulletPool[0].Dequeue();
        go.SetActive(true);
        go.transform.position = transform.position;
        PlayerBullet playerBullet = go.GetComponent<PlayerBullet>();
        playerBullet.ReturnPlayerBulletAction = ReturnPlayerBullet;
    }

    public void CreateEnemyBullet0(Transform transform)
    {
        Vector3 moveDirection = PlayerData.Instance.GetPlayerPosition();
        GameObject go = enemyBulletPool[0].Dequeue();
        go.gameObject.SetActive(true);
        go.transform.position = transform.position;
        EnemyBullet enemyBullet = go.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
    }

    public void CreateEnemyBullet1(Transform transform)
    {
        Vector3 moveDirection = PlayerData.Instance.GetPlayerPosition();
        GameObject go = enemyBulletPool[1].Dequeue();
        go.gameObject.SetActive(true);
        go.transform.position = transform.position;
        EnemyBullet enemyBullet = go.GetComponent<EnemyBullet>();
        enemyBullet.Init(moveDirection);
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
    }

    public void ReturnEnemyBullet(GameObject bullet, int idx)
    {
        bullet.gameObject.SetActive(false);
        enemyBulletPool[idx].Enqueue(bullet);
    }

    public void ReturnPlayerBullet(GameObject bullet, int idx)
    {
        bullet.gameObject.SetActive(false);
        playerBulletPool[idx].Enqueue(bullet);
    }

    public void CreateSingleShot(Transform transform)
    {
        GameObject Go = bossBulletPool[2].Dequeue();
        Go.SetActive(true);
        Go.transform.position = transform.position;

        EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.moveSpeed = 3f;
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
    }

    public void CreateBossDoubleShot(Transform transform)
    {
        GameObject leftGo1 = bossBulletPool[2].Dequeue();
        leftGo1.transform.position = transform.position + new Vector3(-0.3f, 0, 0);
        leftGo1.SetActive(true);

        GameObject leftGo2 = bossBulletPool[2].Dequeue();
        leftGo2.transform.position = transform.position + new Vector3(-0.5f, 0, 0);
        leftGo2.SetActive(true);

        GameObject rightGo1 = bossBulletPool[2].Dequeue();
        rightGo1.transform.position = transform.position + new Vector3(0.3f, 0, 0);
        rightGo1.SetActive(true);

        GameObject rightGo2 = bossBulletPool[2].Dequeue();
        rightGo2.transform.position = transform.position + new Vector3(0.5f, 0, 0);
        rightGo2.SetActive(true);

        EnemyBullet enemyBullet = leftGo1.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
        enemyBullet = leftGo2.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
        enemyBullet = rightGo1.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
        enemyBullet = rightGo2.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
    }

    public void CreateBossShotGun(Transform transform)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Go = bossBulletPool[3].Dequeue();
            Go.SetActive(true);
            Go.transform.position = transform.position;

            Vector3 dirVec = PlayerData.Instance.GetPlayerPosition() - transform.position;
            Vector3 ranVec = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

            dirVec += ranVec;

            EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
            enemyBullet.toMoveDirection = dirVec.normalized;
            enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
        }
    }

    public void CreateBossArcShot(Transform transform, int curCount, int endConut)
    {
        if (curCount == 0)
            return;

        GameObject Go = bossBulletPool[1].Dequeue();
        Go.SetActive(true);
        Go.transform.position = transform.position;

        Vector3 dirVec = new Vector3(Mathf.Sin(Mathf.PI * 10 * curCount / endConut), Mathf.Cos(Mathf.PI * 10 * curCount / endConut), 0);
        EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
        enemyBullet.toMoveDirection = dirVec.normalized;
        enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
    }

    public void CreateBossAroundShot(Transform transform)
    {
        int endCount = 25;

        if (isAroundFlag == false)
        {
            endCount = 25;
            isAroundFlag = true;
        }
        else
        {
            endCount = 20;
            isAroundFlag = false;
        }

        for (int i = 0; i < endCount; i++)
        {
            GameObject Go = bossBulletPool[3].Dequeue();
            Go.SetActive(true);
            Go.transform.position = transform.position;

            Vector3 dirVec = new Vector3(Mathf.Sin(Mathf.PI * 2 * i / endCount), Mathf.Cos(Mathf.PI * 2 * i / endCount), 0);
            EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
            enemyBullet.toMoveDirection = dirVec.normalized;
            enemyBullet.ReturnEnemyBulletAction = ReturnEnemyBullet;
        }
    }
}
