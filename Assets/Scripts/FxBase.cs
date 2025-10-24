using System.Collections;
using UnityEngine;

public class FxBase : MonoBehaviour
{
    private float playTime;

    public Animator fxAnimator;
    void Start()
    {
        playTime = fxAnimator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(Play());
    }
   
    IEnumerator Play()
    {
        yield return new WaitForSeconds(playTime);
        Destroy(this.gameObject);
    }

    public float GetFxEndTime()
    {
        return playTime;
    }

}
