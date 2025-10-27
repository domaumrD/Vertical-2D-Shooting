using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    public float playerSwpanMoveSpeed;
    public int gameScore;
    public bool isGameOver = false;

    public ItemGeratorManager itemGeratorManager;
    public FxManager fxManager;
    public UIManager uiManager;
    public PlayerController playerController;
    public GeneratorEnemyManager generatorEnemyManager;
    public GameObject enemyParent;
    public GameObject enemyBulletParent;

    public GameObject playerSpawnPoint;

    private void Awake()
    {
        uiManager.action = GameOver;

        EventTrigger trigger = uiManager.attackBtn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => playerController.StartShooting());

        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((eventData) => playerController.StopShooting());

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);

        uiManager.boomBtn.onClick.AddListener(() => 
        {
            playerController.UseBoom();
        });

        uiManager.clearAction = ClearGame;

        playerController.hitAction = uiManager.PlayerHit;
        playerController.hitAction += this.Hit;
        playerController.addScoreAction = this.AddScore;
        playerController.useBoomAction = this.UseBoom;
        playerController.shieldAction = this.Shield;
        playerController.getBoomAction = this.GetBoom;
       
    }

    void Start()
    {
        gameScore = 0;
        isGameOver = false;
        uiManager.SetScore(gameScore);
        Application.targetFrameRate = 60;
    }
   

    public void Init(int idx)
    {
        Debug.Log($"로비씬에서 전달 {idx}");
    }

    public void ClearGame()
    {
        generatorEnemyManager.StopGenereEnemy();
        //playerController.StopAction();
        playerController.maxYPos += 9.0f;
        StartCoroutine(EndPlayerStage());

    }

    public void EnemyDie(Vector3 transform)
    {
        StartCoroutine(EnemyDead(transform));
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        isGameOver = true;
        generatorEnemyManager.StopGenereEnemy();
        playerController.StopAction();
    }

    public void AddScore(int score)
    {
        this.gameScore += score;
        uiManager.SetScore(gameScore);
    }
    public void GetBoom()
    {
        int boomcount = playerController.GetPlayerBoomCount();
        uiManager.GetBoom(boomcount - 1);
    }

    public void UseBoom()
    {
        int boomcount = playerController.GetPlayerBoomCount();

        Debug.Log($"GameMain cout : {boomcount}");

        if (boomcount <= 0)
            return;

        fxManager.CreateBoomFx(Vector3.zero);
       
        generatorEnemyManager.DestoryAllEnemy();
        BulletManager.Instance.DestoryAllEnemyBullet();

        uiManager.UseBoom(boomcount - 1);
    }


    public void Shield()
    {
        fxManager.CreateSheildEffect(playerController.shieldPoint);       
    }

    public void Hit()
    {
        playerController.transform.position = playerSpawnPoint.transform.position;
        StartCoroutine(PlayerSpwan());
    }

    IEnumerator PlayerSpwan()
    {
        Shield();
        while (playerController.transform.position.y < -3.5f)
        {
            playerController.transform.Translate(Vector3.up * playerSwpanMoveSpeed * Time.deltaTime);
            yield return null;
        }
        playerController.isHit = false;
    }

    IEnumerator EnemyDead(Vector3 transform)
    {
        yield return new WaitForSeconds(fxManager.CreateExplosionFx(transform));
        itemGeratorManager.CreateItem(transform);
    }

    IEnumerator EndPlayerStage()
    {
        float time = 0f;
        generatorEnemyManager.DestoryAllEnemy();
        BulletManager.Instance.DestoryAllEnemyBullet();
        yield return new WaitForSeconds(2.0f);

        uiManager.gameClearUI.SetActive(true);
        playerController.isgameOver = true;

        while (time < 5.0f)
        {
            time += Time.deltaTime;
            playerController.transform.Translate(Vector3.up * 2.5f * Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene("LobbyScene");
    }

}
