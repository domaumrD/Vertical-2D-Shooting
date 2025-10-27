using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMain : MonoBehaviour
{
    public List<GameObject> player = new List<GameObject>();
    public GameObject runArray;

    public Color selectColor;
    public Color hideColor;

    public RectTransform btnRectTransform;
    public Canvas canvas;
    public Button StartBtn;

    private int selectPlayerIndex = 0;
    private float moveSpeed = 2.0f;

    void Start()
    {       
        SelectPlayerInit();
        SelectPlayer(0);

        StartBtn.gameObject.SetActive(true);

        StartBtn.onClick.AddListener(() =>
        {
            StartBtn.gameObject.SetActive(false);
            Debug.Log(selectPlayerIndex);

            StartCoroutine(Takeoff(player[selectPlayerIndex]));

        });
    }
   
    public void SelectPlayer(int id)
    {
        Debug.Log($"{id}");
        StopAnimaton();
        ColorInit();
        LobbyPlayer selectPlayer = player[id].GetComponent<LobbyPlayer>();
        selectPlayer.LobbyPlayerInit(player.Count);
        selectPlayer.animator.SetBool("isPlay", true);
        selectPlayer.spriteRenderer.color = selectColor;
        runArray.transform.position = new Vector3(player[id].transform.position.x, 0, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player[id].transform.position);
        screenPos.y -= 500f;

        btnRectTransform.position = screenPos;
        selectPlayerIndex = id;
    }

    public void SelectPlayerInit()
    {
        int index = 0;

        for (int i = 0; i < player.Count; i++)
        {
            index = i;
            LobbyPlayer startPlayer = player[index].GetComponent<LobbyPlayer>();
            startPlayer.LobbyPlayerInit(player.Count);
            startPlayer.selectAction = SelectPlayer;

            startPlayer.btn.onClick.AddListener(() => 
            {
                startPlayer.selectAction(startPlayer.playerNum);
            });

        }      
    }

    public void StopAnimaton()
    {
        for (int i = 0; i < player.Count; i++) 
        {
            LobbyPlayer lobbyPlayer = player[i].GetComponent<LobbyPlayer>();
            lobbyPlayer.animator.SetBool("isPlay", false);
        }
    }

    public void ColorInit()
    {
        for (int i = 0; i < player.Count; i++)
        {
            LobbyPlayer lobbyPlayer = player[i].GetComponent<LobbyPlayer>();
            lobbyPlayer.spriteRenderer.color = hideColor;
        }
    }

    IEnumerator Takeoff(GameObject toTakeOff)
    {
        float time = 0f;

        while(time < 2)
        {
            time += Time.deltaTime;
            moveSpeed += 0.4f;
            toTakeOff.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            yield return null;
        }

        AsyncOperation op =  SceneManager.LoadSceneAsync("GameScene");
        op.completed += (opertion) =>
        {
            GameMain gameMain = GameObject.FindFirstObjectByType<GameMain>();
            gameMain.Init(selectPlayerIndex);

        };
    }

}
