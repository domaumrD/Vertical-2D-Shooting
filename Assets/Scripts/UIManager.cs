using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action action;
    public List<GameObject> playerlifes = new List<GameObject>();
    public List<GameObject> playerBooms = new List<GameObject>();
    public GameObject gameOverUI;
    public TMP_Text scoreText;

    public int playerLifeCount;


    void Start()
    {
        playerLifeCount = playerlifes.Count;
        Init();
    }

    public void Init()
    {
        gameOverUI.SetActive(false);

        for (int i = 0; i < playerLifeCount; i++)
        {
            playerlifes[i].SetActive(true);
        }

        playerLifeCount -= 1;
    }

    public void PlayerHit()
    {
        if (playerLifeCount <= -1)
            return;

        Debug.Log($"Life count: {playerLifeCount}");

        playerlifes[playerLifeCount].SetActive(false);
        playerLifeCount--;

        Debug.Log($"Life count: {playerLifeCount}");

        if(playerLifeCount == -1)
        {
            gameOverUI.SetActive(true);
            action();
        }

    }



    public void SetScore(int score)
    {
        string format = string.Format("{0:N0}", score);
        scoreText.text = format;
    }

    public void GetBoom(int boomIndex)
    {
 
        playerBooms[boomIndex].SetActive(true);
        //Debug.Log($"Boom Count: {this.boomCount}");
    }

    public void UseBoom(int boomIndex)
    {
        playerBooms[boomIndex].SetActive(false);
    }

}
