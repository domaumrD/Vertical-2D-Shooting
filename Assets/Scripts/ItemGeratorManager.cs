using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeratorManager : MonoBehaviour
{
    private int selectIndex;
    public List<GameObject> items = new List<GameObject>();
    public int ratio;

    public void CreateItem(Vector3 pos)
    {
        int dice = Random.Range(0, 11);

        if(dice > -1)
        {
            //selectIndex = Random.Range(0, items.Count);
            selectIndex = 0;
            GameObject go = Instantiate(items[selectIndex]);
            go.transform.position = pos;
        }

    }

}
