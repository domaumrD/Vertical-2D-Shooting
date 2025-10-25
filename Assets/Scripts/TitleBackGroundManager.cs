using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGroundManager : MonoBehaviour
{

    public GameObject backGround1;
    public GameObject backGround2;
    public GameObject backGround3;

    public GameObject playerPrefab;
    
    public GameObject bossPrefab;
    public GameObject smallEnemyPrefab;
    public GameObject middleEnemyPrefab;



    public List<Transform> spawn = new List<Transform>();

    public bool isBGcomplete = false;

    void Start()
    {
        GameObject go = Instantiate(backGround1);
        go.GetComponent<TitleBackGround>().action = go.GetComponent<TitleBackGround>().Move1;
        go.GetComponent<TitleBackGround>().chageBackGroundAction = ChangeBG;

    }

    public void ChangeBG(int idx)
    {
        if(idx == 1)
        {
            GameObject bgGo = Instantiate(backGround2);
            bgGo.GetComponent<TitleBackGround>().action = bgGo.GetComponent<TitleBackGround>().Move3;
            bgGo.GetComponent<TitleBackGround>().chageBackGroundAction = ChangeBG;

            GameObject bossGo = Instantiate(bossPrefab);
            bossGo.transform.position = spawn[0].transform.position;
            bossGo.GetComponent<TitleBackGround>().action = bossGo.GetComponent<TitleBackGround>().Move2;
            bossGo.GetComponent<TitleBackGround>().chageBackGroundAction = null;

            for(int i = 1; i < 7; i ++)
            {
                GameObject smallEnemyGo = Instantiate(smallEnemyPrefab);
                smallEnemyGo.transform.position = spawn[i].transform.position;
                smallEnemyGo.GetComponent<TitleBackGround>().action = smallEnemyGo.GetComponent<TitleBackGround>().Move2;
                smallEnemyGo.GetComponent<TitleBackGround>().chageBackGroundAction = null;
            }

            for (int i = 7; i < 11; i++)
            {
                GameObject middleEnemyGo = Instantiate(middleEnemyPrefab);
                middleEnemyGo.transform.position = spawn[i].transform.position;
                middleEnemyGo.GetComponent<TitleBackGround>().action = middleEnemyGo.GetComponent<TitleBackGround>().Move2;
                middleEnemyGo.GetComponent<TitleBackGround>().chageBackGroundAction = null;
            }

        }
        else if (idx == 2)
        {
            GameObject go = Instantiate(backGround3);
            go.GetComponent<TitleBackGround>().action = go.GetComponent<TitleBackGround>().Move7;
            go.GetComponent<TitleBackGround>().chageBackGroundAction = null;

            GameObject playerGo = Instantiate(playerPrefab);
            playerGo.transform.position = spawn[11].transform.position;
            playerGo.GetComponent<TitleBackGround>().action = playerGo.GetComponent<TitleBackGround>().Move5;
            playerGo.GetComponent<TitleBackGround>().chageBackGroundAction = null;

            GameObject bossGo = Instantiate(bossPrefab);
            bossGo.transform.position = spawn[0].transform.position;
            bossGo.GetComponent<TitleBackGround>().action = bossGo.GetComponent<TitleBackGround>().Move4;
            bossGo.GetComponent<TitleBackGround>().chageBackGroundAction = null;

        }
    }

}
