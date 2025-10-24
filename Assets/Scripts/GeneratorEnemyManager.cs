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
    private int dataIdx = 0;

    public GameMain gameMain;
    public GameObject prefabParent;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject bossEnemyPrefab;     
    public List<Transform> spanPoints = new List<Transform>();  
    public List<Spawn> spawnDatas = new List<Spawn>();

    public float delyTime;
    

    void Start()
    {
        delta = 0f;
        enemyIdx = 0;
        isStop = false;

        TextAsset spawnData = Resources.Load<TextAsset>("SpawnData");
        spawnDatas = JsonConvert.DeserializeObject<List<Spawn>>(spawnData.text);
    }

    
    void Update()
    {
        delta += Time.deltaTime;

        if (dataIdx < spawnDatas.Count)
        {
            if (isStop == true || delta < spawnDatas[dataIdx].delay)
                return;

            delta = 0f;
            GenerateEnemy(spawnDatas[dataIdx].point, spawnDatas[dataIdx].delay, spawnDatas[dataIdx].type);
            dataIdx++;
        }

        /*if(isStop == false)
        {
            GenerateEnemy();
        }*/
    }

    public void GenerateEnemy(int point, float delayTime, string type)
    {
        int typeIdx = 0;

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

        GameObject go = Instantiate(enemyPrefabs[typeIdx], prefabParent.transform);
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

            enemyIdx = UnityEngine.Random.Range(0, enemyPrefabs.Count);
            GameObject go = Instantiate(enemyPrefabs[enemyIdx], prefabParent.transform);

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

}
