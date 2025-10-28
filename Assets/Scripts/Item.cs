using System;
using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    public int itemIdx;
    public float moveSpeed;
    public int itemScore;   
    
    public Action<GameObject , int> ReturnItemAction;

    void Update()
    {
        Move();
    }

    public void Move()
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
