using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class TitleBackGround : MonoBehaviour
{
    public Action action;
    public Action<int> chageBackGroundAction;
    public float moveSpeed;
    public Vector3 moveDirection;

    public bool isFadeOut = false;
    public bool isFadeIn = false;

    public SpriteRenderer sprite;
    public Color color;
    public float alpha;

    private void Awake()
    {
        this.sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (action != null)
        {
            action();
        }

        if(this.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
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
                StartCoroutine(fadeOut(1));
            }
        }
    }

    public void Move2()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Move3()
    {
        if(isFadeIn == false)
        {
            StartCoroutine(fadeIn());
            isFadeIn = true;
        }

        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < 2)
        {           
            if (isFadeOut == false)
            {
                isFadeOut = true;
                StartCoroutine(fadeOut(2));
            }
        }
    }

    public void Move4()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y < 3)
        {
            moveSpeed = 0.1f;
            action = Move8;
        }
    }

    public void Move5()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y > 9)
        {
            this.transform.position = new Vector3(this.transform.position.x, -9, this.transform.position.z);
            moveSpeed = 1;
            this.action = Move6;
        }
    }

    public void Move6()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y > -3.6)
        {
            moveDirection = Vector3.down;
            moveSpeed = 0.1f;
        }
        else if(this.transform.position.y < -3.8)
        {
            moveDirection = Vector3.up;
        }
    }

    public void Move7()
    {
        if (isFadeIn == false)
        {
            StartCoroutine(fadeIn());
            isFadeIn = true;
        }

        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.x > 5)
        {
            moveDirection = Vector3.left;
        }
        else if(this.transform.position.x < -5)
        {
            moveDirection = Vector3.right;
        }
    }

    public void Move8()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (this.transform.position.y > 3.0f)
        {
            moveDirection = Vector3.down;
            moveSpeed = 0.1f;
        }
        else if (this.transform.position.y < 2.8)
        {
            moveDirection = Vector3.up;
        }
    }


    IEnumerator fadeOut(int NextBGIndex)
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

        chageBackGroundAction(NextBGIndex);
        Destroy(this.gameObject);
    }

    IEnumerator fadeIn()
    {
        this.color = this.sprite.color;
        this.alpha = this.color.a;
        while (true)
        {
            if (alpha >= 1f)
            {
                break;
            }

            this.alpha += 0.1f;
            this.color.a = this.alpha;
            this.sprite.color = this.color;
            yield return new WaitForSeconds(0.1f);
        }
    }


}
