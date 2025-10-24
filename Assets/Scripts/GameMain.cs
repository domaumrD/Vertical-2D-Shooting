using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

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
   
    void Update()
    {
        
    }

    public void Init(int idx)
    {
        Debug.Log($"{idx}");
    }

    public void EnemyDie(Vector3 transform)
    {
        StartCoroutine(EnemyDead(transform));
    }

    IEnumerator EnemyDead(Vector3 transform)
    {
        yield return new WaitForSeconds(fxManager.CreateExplosionFx(transform));   
        itemGeratorManager.CreateItem(transform);
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

        foreach (Transform child in enemyParent.transform)
        {
            if (child == null)
                continue;

            int tempScore = child.GetComponent<Enemy>().GetScore();
            this.AddScore(tempScore);
            Destroy(child.gameObject);
        }

        foreach (Transform child in enemyBulletParent.transform)
        {
            if (child == null)
                continue;

            Destroy(child.gameObject);
        }

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

}
