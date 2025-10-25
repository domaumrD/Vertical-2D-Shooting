using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemIdx;
    public float moveSpeed;
    public int itemScore;   
    
    public Action<GameObject , int> ReturnItemAction;

    void Update()
    {
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < -7)
        {
            UseCompleteItem();
        }
    }

    public int GetItemScore()
    {
        return itemScore;
    }

    public void UseCompleteItem()
    {
        ReturnItemAction(this.gameObject, itemIdx);
    }
}
