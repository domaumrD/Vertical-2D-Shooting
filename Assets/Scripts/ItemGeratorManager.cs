using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeratorManager : MonoBehaviour
{
    private int selectIndex;
    public List<GameObject> items = new List<GameObject>();
    public Transform itemParent;
    public int ratio;

    public List<Queue<GameObject>> itemPool = new List<Queue<GameObject>>();

    private void Awake()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Queue <GameObject> que = new Queue<GameObject> ();

            for (int j = 0; j < 30; j++)
            {
                GameObject go = Instantiate(items[i],itemParent);
                go.GetComponent<Item>().ReturnItemAction = ReturnItem;
                go.SetActive(false);
                que.Enqueue(go);
            }

            itemPool.Add(que);
        }
    }

    public void CreateItem(Vector3 pos)
    {
        int dice = Random.Range(0, 11);

        if(dice > -1)
        {
            selectIndex = Random.Range(0, items.Count);
            //selectIndex = 0;
            Debug.Log("Create item");
            GameObject go = itemPool[selectIndex].Dequeue();
            go.transform.position = pos;
            go.gameObject.SetActive(true);
        }
    }

    public void ReturnItem(GameObject item, int itemIdx)
    {
        itemPool[itemIdx].Enqueue(item);
        item.gameObject.SetActive(false);
    }
}
