using UnityEngine;

public class FxManager : MonoBehaviour
{
    public GameObject explosionFxPrefab;
    public GameObject boomFxPrefab;

    public GameObject effectShieldPrefab;
    

    public float CreateExplosionFx(Vector3 pos)
    {
        GameObject fxGo = Instantiate(explosionFxPrefab);
        fxGo.transform.position = pos;
        float endTime = fxGo.GetComponent<FxBase>().GetFxEndTime();
        return endTime;
    }

    public float CreateBoomFx(Vector3 pos)
    {
        GameObject fxGo = Instantiate(boomFxPrefab);
        fxGo.transform.position = pos;
        float endTime = fxGo.GetComponent<FxBase>().GetFxEndTime();
        return endTime;
    }

    public void CreateSheildEffect(Transform pos)
    {
        GameObject fxGo = Instantiate(effectShieldPrefab, pos);            
    }

}
