using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
   
    public Vector3 GetPlayerPosition()
    {
        return playerController.transform.position;
    }

}
