using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class TitleBackGround : MonoBehaviour
{
    public Action action;
    public Action<int> chageBackGroundAction;
    public float moveSpeed;
    public Vector3 moveDirection;
    public bool isFadeOut = false;

    public SpriteRenderer sprite;
    public Color color;
    public float alpha;

    private void Awake()
    {
        this.sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        action();
    }

    public void Move1()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if(this.transform.position.x < -6)
        {
            moveDirection = Vector3.down;
            this.moveSpeed = 0.1f;

            if(isFadeOut == false)
            {
                isFadeOut = true;
                StartCoroutine(fadeOut());
            }

        }
    }

    public void Move2()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    IEnumerator fadeOut()
    {
        this.color = this.sprite.color;
        this.alpha = this.color.a;
        while (true)
        {
            if(alpha <= 0)
            {
                break;
            }

            this.alpha -= 0.1f;
            this.color.a = this.alpha;
            this.sprite.color = this.color;
            yield return new WaitForSeconds(0.1f);
        }
        chageBackGroundAction(1);
        Destroy(this.gameObject);
    }

    


}
