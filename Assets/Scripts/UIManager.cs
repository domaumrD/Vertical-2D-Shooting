using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action action;
    public List<GameObject> playerlifes = new List<GameObject>();
    public List<GameObject> playerBooms = new List<GameObject>();
    public GameObject gameOverUI;
    public TMP_Text scoreText;

    public Button lobbyBtn;
    public Button reStartBtn;

    public GameObject bossHpGagueGo;
    public Slider bossHpGauge;

    public int playerLifeCount;
    public float bossHP;
    public float bossMaxHP;

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

        lobbyBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LobbyScene");
        });

        reStartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        lobbyBtn.gameObject.SetActive(false);
        reStartBtn.gameObject.SetActive(false);

        
    }

    public void SetCurBossHP(int hp)
    {
        this.bossHP = hp;
        float cur = bossHP / bossMaxHP;

        Debug.Log(cur);
        bossHpGauge.value = cur;
    }

    public void EndCurBossHP()
    {
        bossHpGagueGo.gameObject.SetActive(false);
    }

    public void SettingBossGauge()
    {
        if (bossMaxHP > 0)
        {
            bossHpGagueGo.SetActive(true);
            StartCoroutine(FillBossHpGauge());
        }
        else
        {
            bossHpGagueGo.SetActive(false);
        }
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
            lobbyBtn.gameObject.SetActive(true);
            reStartBtn.gameObject.SetActive(true);
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

    IEnumerator FillBossHpGauge()
    {
        float cur = 0;     
        while (cur < 1)
        {         
            cur += Time.deltaTime;
            bossHpGauge.value = cur;
            yield return null;
        }        
    }

}
