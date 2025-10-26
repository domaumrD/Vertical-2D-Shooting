using System;
using UnityEngine;

public class BossHpHandler : MonoBehaviour
{
    public UIManager uiManager;
    public GeneratorEnemyManager generatorEnemyManager;

    private int bossMaxHp;

    void Start()
    {
        generatorEnemyManager.MakeBoss = SetBossHpGauge;
    }

    public void SetBossHpGauge()
    {
        bossMaxHp = generatorEnemyManager.GetBossHP();
        GetBossMaxHp();
        BossEnemy boss = generatorEnemyManager.currentBoss.GetComponent<BossEnemy>();

        boss.getHpAction = GetBossCurHp;
        boss.Die = EndBossGauge;
    }

    public void GetBossMaxHp()
    {
        uiManager.bossMaxHP = bossMaxHp;
        uiManager.SettingBossGauge();
    }

    public void GetBossCurHp(int hp)
    {
        Debug.Log($"{hp}");
        uiManager.SetCurBossHP(hp);
    }

    public void EndBossGauge()
    {
        uiManager.EndCurBossHP();
        Debug.Log("Clear !");
    }

}
