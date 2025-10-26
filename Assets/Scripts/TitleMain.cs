using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMain : MonoBehaviour
{
    private int index = 0;
    public Button titleBtn;

    void Start()
    {
        titleBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LobbyScene");

            //AsyncOperation op =  SceneManager.LoadSceneAsync("GameScene");
            //op.completed += (opertion) =>
            //{
            //    GameMain gameMain = GameObject.FindFirstObjectByType<GameMain>();
            //    gameMain.Init(index);

            //};

        });
    }
 
}
