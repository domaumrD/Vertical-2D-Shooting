using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMain : MonoBehaviour
{
    public Button titleBtn;

    void Start()
    {
        titleBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LobbyScene"); 
        });
    }
 
}
