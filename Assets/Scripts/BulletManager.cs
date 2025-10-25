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

    public bool isAroundFlag = true;

    public GameObject playerBullet1Prefab;
    public GameObject enemyBullet0Prefab;
    public GameObject enemyBullet1Prefab;
    public GameObject enemyBulletParent;

    public GameObject bossBullet0Prefab;
    public GameObject bossBullet1Prefab;
    public GameObject bossBullet2Prefab;
    public GameObject bossBullet3Prefab;

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

    public void CreateSingleShot(Transform transform)
    {
        GameObject Go = Instantiate(bossBullet2Prefab, enemyBulletParent.transform);
        Go.transform.position = transform.position;

        EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet.moveSpeed = 3f;

    }

    public void CreateBossDoubleShot(Transform transform)
    {
        GameObject leftGo1 = Instantiate(bossBullet2Prefab, enemyBulletParent.transform);
        leftGo1.transform.position = transform.position + new Vector3(-0.3f,0,0);

        GameObject leftGo2 = Instantiate(bossBullet2Prefab, enemyBulletParent.transform);
        leftGo2.transform.position = transform.position + new Vector3(-0.5f, 0, 0);

        GameObject rightGo1 = Instantiate(bossBullet2Prefab, enemyBulletParent.transform);
        rightGo1.transform.position = transform.position + new Vector3(0.3f, 0, 0);

        GameObject rightGo2 = Instantiate(bossBullet2Prefab, enemyBulletParent.transform);
        rightGo2.transform.position = transform.position + new Vector3(0.5f, 0, 0);

        EnemyBullet enemyBullet = leftGo1.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet = leftGo2.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet = rightGo1.GetComponent<EnemyBullet>();
        enemyBullet.Init();
        enemyBullet = rightGo2.GetComponent<EnemyBullet>();
        enemyBullet.Init();
    }

    public void CreateBossShotGun(Transform transform)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Go = Instantiate(bossBullet3Prefab, enemyBulletParent.transform);
            Go.transform.position = transform.position;

            Vector3 dirVec = PlayerData.Instance.GetPlayerPosition() - transform.position;
            Vector3 ranVec = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

            dirVec += ranVec;

            EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
            enemyBullet.toMoveDirection = dirVec.normalized;
        }
    }

    public void CreateBossArcShot(Transform transform, int curCount, int endConut)
    {
        if (curCount == 0)
            return;

        GameObject Go = Instantiate(bossBullet1Prefab, enemyBulletParent.transform);
        Go.transform.position = transform.position;

        Vector3 dirVec = new Vector3(Mathf.Sin(Mathf.PI * 10 * curCount / endConut), Mathf.Cos(Mathf.PI * 10 * curCount / endConut), 0);
        EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
        enemyBullet.toMoveDirection = dirVec.normalized;
    }

    public void CreateBossAroundShot(Transform transform)
    {
        int endCount = 25;

        if (isAroundFlag ==  false)
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
            GameObject Go = Instantiate(bossBullet3Prefab, enemyBulletParent.transform);
            Go.transform.position = transform.position;

            Vector3 dirVec = new Vector3(Mathf.Sin(Mathf.PI * 2 * i / endCount), Mathf.Cos(Mathf.PI * 2 * i / endCount), 0);
            EnemyBullet enemyBullet = Go.GetComponent<EnemyBullet>();
            enemyBullet.toMoveDirection = dirVec.normalized;
        }


    }


}
