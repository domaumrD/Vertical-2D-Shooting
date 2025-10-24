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

        foreach (Spawn spawn in spawnDatas)
        {
            Debug.Log($"{spawn.point}, {spawn.delay}, {spawn.type}");
        }

        for(dataIdx = 0; dataIdx < spawnDatas.Count; dataIdx++)
        {
            GenerateEnemy(spawnDatas[dataIdx].point, spawnDatas[dataIdx].delay, spawnDatas[dataIdx].type);
        }

    }

    
    void Update()
    {        
        /*if(isStop == false)
        {
            GenerateEnemy();
        }*/
    }

    IEnumerator CreateEnemy(int point, float time, int typeIdx)
    {
        yield return new WaitForSeconds(time);
    }

    public void GenerateEnemy(int point, float delayTime, string type)
    {
        int typeIdx = 0;

        switch(type)
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

        StartCoroutine(CreateEnemy(point,delayTime, typeIdx));
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
