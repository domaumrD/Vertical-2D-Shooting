using System;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    public int playerNum;
    public Animator animator;
    public Action<int> selectAction;

    public SpriteRenderer spriteRenderer;

    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} Å¬¸¯µÊ!");
        selectAction(this.playerNum);
    }   

    public void LobbyPlayerInit(int count)
    {
        int layerIndex = this.playerNum;
        animator.SetLayerWeight(layerIndex, 1f);

        for (int index = 0; index < count; index++)
        {
            if(index == layerIndex)
            {
                continue;
            }

            animator.SetLayerWeight(index, 0f);
        }      
    }
}
