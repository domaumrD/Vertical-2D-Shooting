using System;
using UnityEngine;

public class TitleBackGroundManager : MonoBehaviour
{

    public GameObject backGround1;
    public GameObject backGround2;

    public bool isBGcomplete = false;

    void Start()
    {
        GameObject go = Instantiate(backGround1);
        go.GetComponent<TitleBackGround>().action = go.GetComponent<TitleBackGround>().Move1;
        go.GetComponent<TitleBackGround>().chageBackGroundAction = ChangeBG;

    }

    public void ChangeBG(int idx)
    {
        if(idx == 1)
        {
            GameObject go = Instantiate(backGround2);
            go.GetComponent<TitleBackGround>().action = go.GetComponent<TitleBackGround>().Move2;
            go.GetComponent<TitleBackGround>().chageBackGroundAction = null;
        }
     
    }

}
