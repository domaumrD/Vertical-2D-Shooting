using UnityEngine;

public class Item : MonoBehaviour
{
    public float moveSpeed;
    public int itemScore;   
  
    void Update()
    {
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

    }
    public int GetItemScore()
    {
        return itemScore;
    }
}
