using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnemyManager : MonoBehaviour
{
    private float delta = 0f;
    private int enemyIdx = 0;
    private int enemySpawnIdx = 0;
    private bool isStop = false;
    //private int dataIdx = 0;

    public GameMain gameMain;
    public GameObject prefabParent;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject bossEnemyPrefab;     
    public List<Transform> spanPoints = new List<Transform>();  
    public List<Spawn> spawnDatas = new List<Spawn>();

    public List<Queue<GameObject>> enemyPool = new List<Queue<GameObject>>();

    public GameObject currentBoss = null;
    public float delyTime;
    public Action MakeBoss;

    private void Awake()
    {        
        for(int i = 0; i < enemyPrefabs.Count; i++)
        {
            Queue<GameObject> que = new Queue<GameObject> ();

            for (int j = 0; j < 30; j++)
            {
                GameObject go = Instantiate(enemyPrefabs[i], prefabParent.transform);
                go.gameObject.SetActive(false);
                que.Enqueue(go);

                Enemy enemy = go.GetComponent<Enemy>();
                enemy.action = gameMain.EnemyDie;
                enemy.addScoreAction = gameMain.AddScore;
                enemy.ReturnEnemyAction = ReturnEnemy;
            }

            enemyPool.Add(que);
        }
    }

    void Start()
    {
        delta = 0f;
        enemyIdx = 0;
        isStop = false;
        /*
        TextAsset spawnData = Resources.Load<TextAsset>("SpawnData");
        spawnDatas = JsonConvert.DeserializeObject<List<Spawn>>(spawnData.text);
        */

        GenerateBoss();
    }

    
    void Update()
    {
        /*        
        delta += Time.deltaTime;

        if (dataIdx < spawnDatas.Count)
        {
            if (isStop == true || delta < spawnDatas[dataIdx].delay)
                return;

            delta = 0f;
            GenerateEnemy(spawnDatas[dataIdx].point, spawnDatas[dataIdx].type);
            dataIdx++;
        }
        */

        /*
        if (isStop == false)
        {
            GenerateEnemy();
        }
       */
    }

    public void GenerateBoss()
    {
        GameObject go = Instantiate(bossEnemyPrefab, prefabParent.transform);
        go.transform.position = spanPoints[0].position;
        BossEnemy enemy = go.GetComponent<BossEnemy>();
        enemy.action = gameMain.EnemyDie;
        enemy.addScoreAction = gameMain.AddScore;
        currentBoss = go;
        MakeBoss();
    }

    public int GetBossHP()
    {       
        if (currentBoss != null)
        {
            return currentBoss.GetComponent<BossEnemy>().hp;
        }
        else
        {
            return 0;
        }   
    }

    public void GenerateEnemy(int point, string type)
    {
        int typeIdx = 0;

        if(type == "B")
        {
            GenerateBoss();
            return;
        }

        switch (type)
        {
            case "S":
                typeIdx = 0;
                break;
            case "M":
                typeIdx = 1;
                break;
            case "L":
                typeIdx = 2;
                break;
            case "B":
                typeIdx = 3;
                break;
            default:
                Debug.Log($"Defalut type: {type}");
                typeIdx = 0;
                break;
        }

        Debug.Log($"idx : {typeIdx}");

        GameObject go = enemyPool[typeIdx].Dequeue();
        go.SetActive(true);        
        go.transform.position = spanPoints[point].position;
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.action = gameMain.EnemyDie;
        enemy.addScoreAction = gameMain.AddScore;
    }

    public void GenerateEnemy()
    {
        delta += Time.deltaTime;
        if (delta > delyTime)
        {
            delta = 0f;
            Debug.Log("create Enemy");
            enemyIdx = UnityEngine.Random.Range(0, enemyPrefabs.Count);
            GameObject go = enemyPool[enemyIdx].Dequeue();
            go.SetActive(true);

            Debug.Log($"{go.gameObject.name}"); 
            enemySpawnIdx = UnityEngine.Random.Range(0, spanPoints.Count);
            go.transform.position = spanPoints[enemySpawnIdx].position;

            Enemy enemy = go.GetComponent<Enemy>();
            enemy.action = gameMain.EnemyDie;
            enemy.addScoreAction = gameMain.AddScore;
                        
        }
    }

    public void StopGenereEnemy()
    {
        isStop = true;
    }

    public void ReturnEnemy(GameObject enemyGo, int enemyIdx)
    {
        enemyPool[enemyIdx].Enqueue(enemyGo);
        enemyGo.gameObject.SetActive(false);
    }

    public void DestoryAllEnemy()
    {
        Enemy[] enemys = prefabParent.GetComponentsInChildren<Enemy>(true);

        if (enemys == null)
            return;

        foreach (Enemy enemy in enemys)
        {
            if (enemy.gameObject.activeSelf == true)
            {
                enemy.ReturnEnemyPool();
            }
        }
    }

}
